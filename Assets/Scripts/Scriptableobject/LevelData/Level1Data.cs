using System.Collections;
using System.Collections.Generic;
using CustomUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level1 {
    [CreateAssetMenu(fileName = "Level1Data", menuName = "LevelData/Level1Data")]
    public class Level1Data : LevelData {
        [SerializeField]
        [Scene]
        private string levelScene;
        [SerializeField]
        private Canvas chordLibraryUIPrefab;
        [SerializeField]
        private ChordEditor.ChordEditor chordEditorPrefab;
        [SerializeField]
        private Level1ControlPanel controlPanelPrefab;
        [SerializeField]
        private ProgressGrid.ChordProgressGridUI chordProgressGridPrefab;
        [SerializeField]
        private ChordEditor.ChordEditorController chordEditorController;
        [SerializeField]
        private Managers.MusicSystem musicSystem;
        [SerializeField]
        private Tutorial.TutorialController tutorialController;

        private ProgressGrid.ChordProgressGridUI chordProgressGrid;
        private Level1ControlPanel controlPanel;

        private void CheckChord(int pos) {
            ChordName? chord = chordEditorController.GetChord(pos);
            if (chord != null) {
                if (chordProgressGrid.TryToRevealChord(pos, (ChordName)chord)) {
                    if (chordProgressGrid.CheckIfComplete()) {
                        CompleteLevel();
                    }
                }
            }
        }

        private void CompleteLevel() {
            Debug.Log("Level completed");
        }

        private IEnumerator LoadAsync() {
            var asyncLoad = SceneManager.LoadSceneAsync(levelScene);
            while (!asyncLoad.isDone) {
                yield return null;
            }

            musicSystem.Init();
            chordEditorController.Init(chordLibraryUIPrefab, chordEditorPrefab, musicSystem);

            controlPanel = Instantiate(controlPanelPrefab);
            controlPanel.PlayButtonPressed.AddListener(musicSystem.PlayPause);
            controlPanel.StopButtonPressed.AddListener(musicSystem.Stop);

            chordEditorController.PointerPosChanged.AddListener((int pos) => musicSystem.StartBlock = pos);
            musicSystem.Stopped.AddListener(() => controlPanel.SetPlaySelected(false));

            musicSystem.Started.AddListener(chordEditorController.StartPointerControl);
            musicSystem.BlockChanged.AddListener(chordEditorController.MoveControlledPointer);
            musicSystem.Stopped.AddListener(chordEditorController.EndPointerControl);

            chordProgressGrid = Instantiate(chordProgressGridPrefab);
            musicSystem.BlockChanged.AddListener(CheckChord);

            tutorialController.Init();
        }

        public void ChangePointerPos() {

        }

        public override void Load() {
            EntryPoint.Instance.StartCoroutine(LoadAsync());
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
