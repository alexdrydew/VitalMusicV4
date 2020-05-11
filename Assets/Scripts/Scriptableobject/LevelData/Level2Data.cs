using System.Collections;
using CustomUI;
using Managers;
using NoteEditor;
using UnityEngine;

namespace Level2 {
    [CreateAssetMenu(fileName = "Level2Data", menuName = "LevelData/Level2Data")]
    public class Level2Data : MusicLevelData {
        private ExtendedLevelControlPanel extendedControlPanel;

        [SerializeField]
        private ExtendedLevelControlPanel extendedControlPanelPrefab;

        [SerializeField]
        private MusicSystem ghostPlayMusicSystem;

        [SerializeField]
        private NoteEditorController noteEditorController;

        private void CheckNote(int pos) {
            //TODO
        }

        private void AssignControlPanelControls() {
            extendedControlPanel.PlayButtonPressed.AddListener(musicSystem.PlayPause);
            extendedControlPanel.PlayButtonPressed.AddListener(_ => {
                                                                   ghostPlayMusicSystem.Stop();
                                                                   noteEditorController
                                                                       .StartPointerControl(); //cause Stop() ends pointer control
                                                               });

            extendedControlPanel.AlternativePlayPressed.AddListener(ghostPlayMusicSystem.PlayPause);
            extendedControlPanel.AlternativePlayPressed.AddListener(_ => {
                                                                        musicSystem.Stop();
                                                                        noteEditorController
                                                                            .StartPointerControl(); //cause Stop() ends pointer control
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

        protected override IEnumerator LoadAsync() {
            yield return base.LoadAsync();

            musicSystem.Init();
            ghostPlayMusicSystem.Init();

            extendedControlPanel = Instantiate(extendedControlPanelPrefab);
            noteEditorController.Init();

            AssignControlPanelControls();
            AssignMusicSystemControls();
            noteEditorController.PointerPosChanged.AddListener(pos => musicSystem.StartBlock = pos);
        }

        public override void Unload() {
            musicSystem.Destroy();
            ghostPlayMusicSystem.Destroy();
            noteEditorController.Destroy();
            Destroy(extendedControlPanel);
        }
    }
}