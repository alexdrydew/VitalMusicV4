using Managers;
using UnityEngine;

namespace ChordEditor {
    [CreateAssetMenu(fileName = "ChordEditorController", menuName = "ChordEditor/ChordEditorController")]
    public class ChordEditorController : EditorController {
        private ChordEditor chordEditor;

        [SerializeField]
        private ChordEventManager chordEventManager;

        [SerializeField]
        private GameEvent.GameEvent ChordPlacedEvent;

        private ChordsLibraryUI libraryUI;

        [SerializeField]
        private GameEvent.GameEvent PointerPosChangedEvent;

        private void InitLibrary(Canvas libraryUIPrefab) {
            Canvas libraryUICanvas = Instantiate(libraryUIPrefab);
            libraryUICanvas.worldCamera = Camera.main;
            libraryUICanvas.planeDistance = 9f;
            libraryUI = libraryUICanvas.GetComponentInChildren<ChordsLibraryUI>();
        }

        public void Init(Canvas libraryUIPrefab, ChordEditor chordEditorPrefab, MusicSystem musicSystem) {
            InitLibrary(libraryUIPrefab);

            chordEditor = Instantiate(chordEditorPrefab);
            base.Init(chordEditor);

            chordEditor.ChordsUpdated.AddListener(UpdateChords);
            chordEventManager.Init();

            PointerPosChanged.AddListener(_ => PointerPosChangedEvent.Invoke());
            chordEditor.ChordsUpdated.AddListener(ChordPlacedEvent.Invoke);
        }

        public ChordName? GetChord(int pos) {
            return chordEditor.CurrentNames[pos];
        }

        private void UpdateChords() {
            chordEventManager.UpdateChords(chordEditor.CurrentNames);
        }

        public override void Destroy() {
            Destroy(libraryUI);
            Destroy(chordEditor);
        }
    }
}