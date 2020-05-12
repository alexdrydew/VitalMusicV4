using System.Collections;
using ChordEditor;
using CustomUI;
using Managers;
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
        private Canvas chordLibraryUiPrefab;

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
            if (chord == null) return;
            if (!chordProgressGrid.TryToRevealChord(pos, (ChordName) chord)) return;
            if (chordProgressGrid.CheckIfComplete())
                CompleteLevel();
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

        private IEnumerator LoadAsync() {
            yield return LoadScene();
            LoadMenu();
            AssignMenu();
            
            musicSystem.Init();
            
            chordProgressGrid = Instantiate(chordProgressGridPrefab);
            chordEditorController.Init(chordLibraryUiPrefab, chordEditorPrefab, musicSystem);
            tutorialController.Init();

            AssignControlPanelControls();
            AssignMusicSystemControls();
            chordEditorController.PointerPosChanged.AddListener(pos => musicSystem.StartBlock = pos);
        }

        public override void Load() {
            EntryPoint.Instance.StartCoroutine(LoadAsync());
        }

        public override void Unload() {
            base.Unload();
            RemoveMenuAssignment();
            musicSystem.Destroy();
            chordEditorController.Destroy();
            tutorialController.Destroy();
            Destroy(controlPanel);
            Destroy(chordProgressGrid);
        }
    }
}