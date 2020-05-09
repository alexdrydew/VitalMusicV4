using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChordEditor {
    [CreateAssetMenu(fileName = "ChordEditorController", menuName = "ChordEditor/ChordEditorController")]
    public class ChordEditorController : ScriptableObject {
        [SerializeField]
        private ChordEventManager chordEventManager;
        [SerializeField]
        private GameEvent.GameEvent PointerPosChangedEvent;
        [SerializeField]
        private GameEvent.GameEvent ChordPlacedEvent;

        private Managers.MusicSystem musicSystem;
        private ChordsLibraryUI libraryUI;
        private ChordEditor chordEditor;

        public EditorPointerPosChanged PointerPosChanged => chordEditor.PointerPosChanged;

        private void InitLibrary(Canvas libraryUIPrefab) {
            var libraryUICanvas = Instantiate(libraryUIPrefab);
            libraryUICanvas.worldCamera = Camera.main;
            libraryUICanvas.planeDistance = 9f;
            libraryUI = libraryUICanvas.GetComponentInChildren<ChordsLibraryUI>();
        }

        public void Init(Canvas libraryUIPrefab, ChordEditor chordEditorPrefab, Managers.MusicSystem musicSystem) {
            InitLibrary(libraryUIPrefab);

            this.musicSystem = musicSystem;

            chordEditor = Instantiate(chordEditorPrefab);
            chordEditor.ChordsUpdated.AddListener(UpdateChords);
            chordEventManager.Init(musicSystem);

            PointerPosChanged.AddListener(_ => PointerPosChangedEvent.Invoke());
            chordEditor.ChordsUpdated.AddListener(ChordPlacedEvent.Invoke);
        }

        public ChordName? GetChord(int pos) {
            return chordEditor.CurrentNames[pos];
        }

        public void StartPointerControl() {
            chordEditor.SavePointerPos();
        }

        public void MoveControlledPointer(int pos) {
            chordEditor.MovePointerTo(pos);
        }

        public void EndPointerControl() {
            chordEditor.RestorePointerPos();
        }

        private void UpdateChords() {
            chordEventManager.UpdateChords(chordEditor.CurrentNames);
        }

        public void Destroy() {
            Destroy(libraryUI);
            Destroy(chordEditor);
        }

    }
}
