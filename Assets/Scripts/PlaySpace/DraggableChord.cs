using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public static partial class Helpers {
    public static Vector3 AddZ(Vector2 vec, float z) {
        return new Vector3(vec.x, vec.y, z);
    }
}

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class DraggableChord : MonoBehaviour, IDragHandler, IEndDragHandler {
    public static DraggableChord Instantiate(DraggableChord prefab,
            Transform parent, Camera camera, ChordName chord, ChordSlot slot) {
        var instance = Instantiate(prefab, parent);
        instance.name = chord.ToString();
        instance.camera = camera;
        instance.Chord = chord;
        instance.Slot = slot;
        instance.isProperlyInstantiated = true;
        return instance;
    }

    private bool isProperlyInstantiated = false;

    private TextMeshPro text;
    private ChordName chord;
    public ChordName Chord {
        get => chord;
        set {
            this.chord = value;
            text.SetText(chord != ChordName.Hidden ? value.ToString() : "?");
        }
    }

    public ChordSlot Slot { get; set; }

    private new Camera camera;

    private void Awake() {
        text = GetComponentInChildren<TextMeshPro>();
    }

    private void Start() {
        if (!isProperlyInstantiated) {
            throw new InstantiationException<DraggableChord>();
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            transform.position = Helpers.AddZ(camera.ScreenToWorldPoint(eventData.position), -1);
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Slot"));
            List<RaycastHit2D> results = new List<RaycastHit2D>();
            Physics2D.Raycast(camera.ScreenToWorldPoint(eventData.position), Vector2.zero, filter, results);
            if (results.Count > 0) {
                ChordSlot slot = results[0].transform.GetComponent<ChordSlot>();
                if (slot != null && slot != Slot) {
                    this.Slot.FreeChord();
                    slot.AttachChord(this);
                }
                transform.localPosition = new Vector3(0, 0, -0.1f);
            } else {
                Slot.FreeChord();
                Destroy(gameObject);
            }
        }
    }
}
