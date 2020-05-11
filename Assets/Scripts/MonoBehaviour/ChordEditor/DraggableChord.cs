using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChordEditor {
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class DraggableChord : MonoBehaviour, IDragHandler, IEndDragHandler {
        private new Camera camera;
        private ChordName chord;

        private bool isProperlyInstantiated;

        private TextMeshPro text;

        public ChordName Chord {
            get => chord;
            set {
                chord = value;
                text.SetText(chord != ChordName.Hidden ? value.ToString() : "?");
            }
        }

        public ChordSlot Slot { get; set; }

        public void OnDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left)
                transform.position = Helpers.ReplaceZ(camera.ScreenToWorldPoint(eventData.position), -5f);
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                var filter = new ContactFilter2D();
                filter.SetLayerMask(LayerMask.GetMask("Slot"));
                var results = new List<RaycastHit2D>();
                Physics2D.Raycast(camera.ScreenToWorldPoint(eventData.position), Vector2.zero, filter, results);
                if (results.Count > 0) {
                    var slot = results[0].transform.GetComponent<ChordSlot>();
                    if (slot != null && slot != Slot) {
                        if (Slot != null) Slot.FreeChord();
                        slot.AttachChord(this);
                    }

                    transform.localPosition = new Vector3(0, 0, -0.1f);
                }
                else {
                    if (Slot != null) Slot.FreeChord();
                    Destroy(gameObject);
                }
            }
        }

        public static DraggableChord Instantiate(DraggableChord prefab,
            Transform parent, Camera camera, ChordName chord, ChordSlot slot) {
            DraggableChord instance = Instantiate(prefab, parent);
            instance.name = chord.ToString();
            instance.camera = camera;
            instance.Chord = chord;
            instance.Slot = slot;
            instance.isProperlyInstantiated = true;
            return instance;
        }

        public static DraggableChord Instantiate(DraggableChord prefab, Camera camera, ChordName chord) {
            DraggableChord instance = Instantiate(prefab);
            instance.name = chord.ToString();
            instance.camera = camera;
            instance.Chord = chord;
            instance.isProperlyInstantiated = true;
            return instance;
        }

        private void Awake() {
            text = GetComponentInChildren<TextMeshPro>();
        }

        private void Start() {
            if (!isProperlyInstantiated) throw new InstantiationException<DraggableChord>();
        }
    }
}