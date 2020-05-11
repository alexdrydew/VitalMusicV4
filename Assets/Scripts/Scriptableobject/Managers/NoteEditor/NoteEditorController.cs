using UnityEngine;

namespace NoteEditor {
    [CreateAssetMenu(fileName = "NoteEditorController", menuName = "NoteEditor/NoteEditorController")]
    public class NoteEditorController : EditorController {
        private NoteEditor noteEditor;

        [SerializeField]
        private NoteEditor noteEditorPrefab;

        [SerializeField]
        private NoteEventManager noteEventManager;

        public void Init() {
            noteEditor = Instantiate(noteEditorPrefab);
            base.Init(noteEditor);

            noteEditor.NoteEditorUpdated.AddListener(UpdateNotes);

            noteEventManager.Init();
        }

        private void UpdateNotes() {
            noteEventManager.UpdateNotes(noteEditor.CurrentNotes);
        }

        public override void Destroy() {
            Destroy(noteEditor);
        }
    }
}