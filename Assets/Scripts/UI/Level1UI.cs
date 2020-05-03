﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI {
    public class Level1UI : LevelUI,
        IHaveChordLibrary, IInteractWithMusicSystem, IHavePlayButton, IHaveStopButton {
        [SerializeField]
        private CustomUI.SelectableButton playButton;
        [SerializeField]
        private CustomUI.Button stopButton;
        [SerializeField]
        private ProgressGridUI progressGrid;
        [SerializeField]
        private RectTransform chordsLibraryRoot;
        [SerializeField]
        private CustomUI.Button chordsLibraryButton;
        private MusicSystem musicSystem;


        public CustomUI.SelectableButtonPressedEvent PlayButtonPressed { get => playButton.PressedSelectable; }
        public CustomUI.ButtonPressedEvent StopButtonPressed { get => stopButton.Pressed; }

        public MusicSystem MusicSystem { get => musicSystem; private set => musicSystem = value; }
        public ButtonPressedEvent ChordsLibraryButtonPressed { get => chordsLibraryButton.Pressed; }
        public RectTransform ChordsLibraryRoot { get => chordsLibraryRoot; }

        public override void Init(Camera camera, MusicSystem musicSystem) {
            Camera = camera;
            MusicSystem = musicSystem;

            musicSystem.Stopped.AddListener(DeselectPlayButton);
            musicSystem.Started.AddListener(SelectPlayButton);
        }

        private void Awake() {
            if (playButton == null || stopButton == null || progressGrid == null) {
                throw new DataException<Level1UI>();
            }
        }

        private void OnDestroy() {
            if (musicSystem != null) {
                musicSystem.Stopped.RemoveListener(DeselectPlayButton);
                musicSystem.Started.RemoveListener(SelectPlayButton);
            }
        }

        private void DeselectPlayButton() {
            playButton.IsSelected = false;
        }

        private void SelectPlayButton() {
            playButton.IsSelected = true;
        }

        public void TryToRevealChord(int index, ChordName guessedChord) {
            progressGrid.TryToRevealChord(index, guessedChord);
        }

        public override bool CheckIfComplete() {
            return progressGrid.CheckIfComplete();
        }
    }
}