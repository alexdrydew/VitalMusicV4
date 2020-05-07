using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class ChordSlot : MonoBehaviour
{
    public static ChordSlot Instantiate(ChordSlot prefab,
        Transform parent, Vector3 localPosition, Camera camera,
        DraggableChord chordPrefab, ChordEditor chordEditor) {
        var instance = Instantiate(prefab, parent);
        instance.transform.localPosition = Helpers.AddZ(localPosition, -0.1f);
        instance.camera = camera;
        instance.chordPrefab = chordPrefab;
        instance.editor = chordEditor;
        instance.isProperlyInstantiated = true;
        return instance;
    }

    private DraggableChord chordPrefab;
    private DraggableChord attached;
    private ChordEditor editor;
    private new Camera camera;
    public DraggableChord Attached { get => attached; private set => attached = value; }

    private bool isProperlyInstantiated = false;

    private void Start() {
        if (!isProperlyInstantiated) {
            throw new InstantiationException<ChordSlot>();
        }
    }

    public void AttachChord(ChordName chord) {
        AttachChord(DraggableChord.Instantiate(chordPrefab, transform, camera, chord, this));
    }

    public void AttachChord(DraggableChord chord) {
        if (Attached != null) {
            Destroy(Attached.gameObject);
        }
        chord.transform.parent = transform;
        Attached = chord;
        chord.Slot = this;
        editor.UpdateMusicEvents();

        GlobalEventsManager.Invoke(GlobalEventType.ChordPlacedEvent, this);
    }

    public void FreeChord() {
        Attached = null;
    }
}
