using ProgressGrid;
using UnityEngine;

namespace NoteEditor {
    [CreateAssetMenu(fileName = "NoteEditorData", menuName = "NoteEditor/NoteEditorData")]
    public class NoteEditorData : ScriptableObject {
        [SerializeField]
        private float contentMargin;

        [SerializeField]
        private NoteProgressGridUi noteProgressGridUIPrefab;

        [SerializeField]
        private NoteSelector noteSelectorPrefab;

        public NoteSelector NoteSelectorPrefab => noteSelectorPrefab;
        public float ContentMargin => contentMargin;

        public NoteProgressGridUi NoteProgressGridUiPrefab => noteProgressGridUIPrefab;
    }
}