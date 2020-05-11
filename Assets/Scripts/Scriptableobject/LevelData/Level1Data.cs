using System.Collections;
using ChordEditor;
using CustomUI;
using ProgressGrid;
using Tutorial;
using UnityEngine;

namespace Level1 {
    [CreateAssetMenu(fileName = "Level1Data", menuName = "LevelData/Level1Data")]
    public class Level1Data : MusicLevelData {
        [SerializeField]
        private ChordEditorController chordEditorController;

        [SerializeField]
        private ChordEditor.ChordEditor chordEditorPrefab;

        [SerializeField]
        private Canvas chordLibraryUIPrefab;

        private ChordProgressGridUI chordProgressGrid;

        [SerializeField]
        private ChordProgressGridUI chordProgressGridPrefab;

        private LevelControlPanel controlPanel;

        [SerializeField]
        private LevelControlPanel controlPanelPrefab;

        [SerializeField]
        private TutorialController tutorialController;

        private void CheckChord(int pos) {
            ChordName? chord = chordEditorController.GetChord(pos);
            if (chord != null)
                if (chordProgressGrid.TryToRevealChord(pos, (ChordName) chord))
                    if (chordProgressGrid.CheckIfComplete())
                        CompleteLevel();
        }

        private void CompleteLevel() {
            Debug.Log("Level completed");
        }

        private void AssignControlPanelControls() {
            controlPanel = Instantiate(controlPanelPrefab);
            controlPanel.PlayButtonPressed.AddListener(musicSystem.PlayPause);
            controlPanel.StopButtonPressed.AddListener(musicSystem.Stop);
        }

        private void AssignMusicSystemControls() {
            musicSystem.Stopped.AddListener(() => controlPanel.SetPlaySelected(false));
            musicSystem.Started.AddListener(chordEditorController.StartPointerControl);
            musicSystem.BlockChanged.AddListener(chordEditorController.MoveControlledPointer);
            musicSystem.Stopped.AddListener(chordEditorController.EndPointerControl);
            musicSystem.BlockChanged.AddListener(CheckChord);
        }

        protected override IEnumerator LoadAsync() {
            yield return base.LoadAsync();

            chordProgressGrid = Instantiate(chordProgressGridPrefab);
            chordEditorController.Init(chordLibraryUIPrefab, chordEditorPrefab, musicSystem);
            tutorialController.Init();

            AssignControlPanelControls();
            AssignMusicSystemControls();
            chordEditorController.PointerPosChanged.AddListener(pos => musicSystem.StartBlock = pos);
        }

        public override void Unload() {
            musicSystem.Destroy();
            chordEditorController.Destroy();
            tutorialController.Destroy();
            Destroy(controlPanel);
            Destroy(chordProgressGrid);
        }
    }
}