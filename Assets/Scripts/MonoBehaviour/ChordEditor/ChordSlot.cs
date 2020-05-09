using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChordEditor {
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class ChordSlot : MonoBehaviour {
        public static ChordSlot Instantiate(ChordSlot prefab,
            Transform parent, Vector3 localPosition, DraggableChord chordPrefab, ChordEditor chordEditor) {
            var instance = Instantiate(prefab, parent);
            instance.transform.localPosition = Helpers.ReplaceZ(localPosition, -0.1f);
            instance.chordPrefab = chordPrefab;
            instance.editor = chordEditor;

            instance.camera = Camera.main;

            instance.isProperlyInstantiated = true;
            return instance;
        }

        private DraggableChord chordPrefab;
        private DraggableChord attached;
        private ChordEditor editor;
        private new Camera camera;

        public ChordAttachedToSlotEvent ChordAttached { get; private set; }
        public DraggableChord Attached { get => attached; private set => attached = value; }

        private bool isProperlyInstantiated = false;

        private void Awake() {
            if (ChordAttached == null) {
                ChordAttached = new ChordAttachedToSlotEvent();
            }
        }

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
            ChordAttached.Invoke();
        }

        public void FreeChord() {
            Attached = null;
        }
    }
}
