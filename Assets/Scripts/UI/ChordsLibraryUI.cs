using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CustomUI {
    public class ChordsLibraryUI : MonoBehaviour {
        [SerializeField]
        private List<ChordName> initialChords;
        [SerializeField]
        private float toggleTime = 0.5f;
        [SerializeField]
        private DraggableChordUI chordPrefab;
        [SerializeField]
        private LevelUI uiRoot;

        private bool isOpened = false;

        private void Awake() {
            if (chordPrefab == null) {
                throw new DataException<ChordsLibraryUI>();
            }
            foreach (var chord in initialChords) {
                AddChord(chord);
            }

            (uiRoot as IHaveChordLibrary).ChordsLibraryButtonPressed.AddListener(Toggle);
        }

        private void Toggle() {
            RectTransform curPos = (uiRoot as IHaveChordLibrary).ChordsLibraryRoot;
            if (isOpened) {
                LeanTween.moveX(curPos, curPos.anchoredPosition.x + curPos.rect.width, toggleTime);
            } else {
                LeanTween.moveX(curPos, curPos.anchoredPosition.x - curPos.rect.width, toggleTime);
            }
            GlobalEventsManager.ChordLibraryOpened.Invoke();
            isOpened = !isOpened;
        }

        public void AddChord(ChordName chord) {
            DraggableChordUI.Instantiate(chordPrefab, transform, chord, uiRoot);
        }
    }
}
