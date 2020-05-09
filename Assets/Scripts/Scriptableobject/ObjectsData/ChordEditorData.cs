using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChordEditor {
    [CreateAssetMenu(fileName = "ChordEditorData", menuName = "ChordEditor/ChordEditorData")]
    public class ChordEditorData : ScriptableObject {
        [SerializeField]
        private DraggableChord chordPrefab;
        [SerializeField]
        private ChordSlot chordSlotPrefab;
        [SerializeField]
        private EditorPointer editorPointerPrefab;
        [SerializeField]
        private Vector3 origin;
        [SerializeField]
        private float contentMargin;

        public DraggableChord ChordPrefab => chordPrefab;
        public ChordSlot ChordSlotPrefab => chordSlotPrefab;
        public Vector3 Origin => origin;
        public float ContentMargin => contentMargin;
    }
}
