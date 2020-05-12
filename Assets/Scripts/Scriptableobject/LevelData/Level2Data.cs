using System.Collections;
using CustomUI;
using Managers;
using NoteEditor;
using Tutorial;
using UnityEngine;

namespace Level2 {
    [CreateAssetMenu(fileName = "Level2Data", menuName = "LevelData/Level2Data")]
    public class Level2Data : MusicLevelData {

        [SerializeField]
        private ExtendedLevelControlPanel extendedControlPanelPrefab;

        [SerializeField]
        private MusicSystem ghostPlayMusicSystem;

        [SerializeField]
        private NoteEditorController noteEditorController;
        
        [SerializeField]
        private TutorialController tutorialController;

        private ExtendedLevelControlPanel extendedControlPanel;
        
        private void CheckNote(int pos) {
            NoteName? note = noteEditorController.GetNote(pos)?.NoteName;
            if (note == null) return;
            if (!noteEditorController.NoteProgressGridUi.TryToRevealNote(pos, (NoteName) note)) return;
            if (!noteEditorController.NoteProgressGridUi.CheckIfComplete()) return;
            CompleteLevel();
        }

        private void AssignControlPanelControls() {
            extendedControlPanel.PlayButtonPressed.AddListener(state => {
                                                                   ghostPlayMusicSystem.Stop();
                                                                   noteEditorController
                                                                       .StartPointerControl(); //cause Stop() ends pointer control
                                                                   musicSystem.PlayPause(state);
                                                               });

            extendedControlPanel.AlternativePlayPressed.AddListener(state => {
                                                                        musicSystem.Stop();
                                                                        noteEditorController
                                                                            .StartPointerControl(); //cause Stop() ends pointer control
                                                                        ghostPlayMusicSystem.PlayPause(state);
                                                                    });

            extendedControlPanel.StopButtonPressed.AddListener(musicSystem.Stop);
            extendedControlPanel.StopButtonPressed.AddListener(ghostPlayMusicSystem.Stop);
        }

        private void AssignMusicSystemControls() {
            musicSystem.Stopped.AddListener(() => extendedControlPanel.SetPlaySelected(false));
            ghostPlayMusicSystem.Stopped.AddListener(() => extendedControlPanel.SetAlternativePlaySelected(false));

            noteEditorController.PointerPosChanged.AddListener(pos => musicSystem.StartBlock = pos);
            noteEditorController.PointerPosChanged.AddListener(pos => ghostPlayMusicSystem.StartBlock = pos);

            musicSystem.Started.AddListener(noteEditorController.StartPointerControl);
            ghostPlayMusicSystem.Started.AddListener(noteEditorController.StartPointerControl);

            musicSystem.BlockChanged.AddListener(noteEditorController.MoveControlledPointer);
            ghostPlayMusicSystem.BlockChanged.AddListener(noteEditorController.MoveControlledPointer);

            musicSystem.Stopped.AddListener(noteEditorController.EndPointerControl);
            ghostPlayMusicSystem.Stopped.AddListener(noteEditorController.EndPointerControl);

            musicSystem.BlockChanged.AddListener(CheckNote);
        }

        private IEnumerator LoadAsync() {
            yield return LoadScene();
            LoadMenu();
            AssignMenu();

            musicSystem.Init();
            ghostPlayMusicSystem.Init();
            
            extendedControlPanel = Instantiate(extendedControlPanelPrefab);
            noteEditorController.Init();

            AssignControlPanelControls();
            AssignMusicSystemControls();
            noteEditorController.PointerPosChanged.AddListener(pos => musicSystem.StartBlock = pos);
            
            tutorialController.Init();
        }

        public override void Load() {
            EntryPoint.Instance.StartCoroutine(LoadAsync());
        }

        public override void Unload() {
            base.Unload();
            RemoveMenuAssignment();
            musicSystem.Destroy();
            ghostPlayMusicSystem.Destroy();
            noteEditorController.Destroy();
            Destroy(extendedControlPanel);
        }
    }
}