using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ChordEditor {
    public class ChordsLibraryUI : MonoBehaviour {
        [SerializeField]
        private GameEvent.GameEvent chordLibraryOpenedEvent;
        [SerializeField]
        private List<ChordName> initialChords;
        [SerializeField]
        private CustomUI.Button button;
        [SerializeField]
        private float toggleTime = 0.5f;
        [SerializeField]
        private DraggableChordUI chordPrefab;
        [SerializeField]
        private Transform content;

        private bool isOpened = false;
        private RectTransform rectTransform;

        private void Awake() {
            if (chordPrefab == null) {
                throw new DataException<ChordsLibraryUI>();
            }
            foreach (var chord in initialChords) {
                AddChord(chord);
            }

            rectTransform = GetComponent<RectTransform>();
        }

        private void Start() {
            button.Pressed.AddListener(Toggle);
        }

        public void Toggle() {
            if (isOpened) {
                LeanTween.moveX(rectTransform, 
                    rectTransform.anchoredPosition.x + rectTransform.rect.width, toggleTime);
            } else {
                LeanTween.moveX(rectTransform, 
                    rectTransform.anchoredPosition.x - rectTransform.rect.width, toggleTime);
            }
            chordLibraryOpenedEvent.Invoke();
            isOpened = !isOpened;
        }

        public void AddChord(ChordName chord) {
            DraggableChordUI.Instantiate(chordPrefab, content, chord);
        }
    }
}
