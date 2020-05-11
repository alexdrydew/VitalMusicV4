using System.Collections.Generic;
using CustomUI;
using UnityEngine;

namespace ChordEditor {
    public class ChordsLibraryUI : MonoBehaviour {
        [SerializeField]
        private Button button;

        [SerializeField]
        private GameEvent.GameEvent chordLibraryOpenedEvent;

        [SerializeField]
        private DraggableChordUI chordPrefab;

        [SerializeField]
        private Transform content;

        [SerializeField]
        private List<ChordName> initialChords;

        private bool isOpened;
        private RectTransform rectTransform;

        [SerializeField]
        private float toggleTime = 0.5f;

        private void Awake() {
            if (chordPrefab == null) throw new DataException<ChordsLibraryUI>();
            foreach (ChordName chord in initialChords) AddChord(chord);

            rectTransform = GetComponent<RectTransform>();
        }

        private void Start() {
            button.Pressed.AddListener(Toggle);
        }

        public void Toggle() {
            if (isOpened)
                LeanTween.moveX(rectTransform,
                    rectTransform.anchoredPosition.x + rectTransform.rect.width, toggleTime);
            else
                LeanTween.moveX(rectTransform,
                    rectTransform.anchoredPosition.x - rectTransform.rect.width, toggleTime);
            chordLibraryOpenedEvent.Invoke();
            isOpened = !isOpened;
        }

        public void AddChord(ChordName chord) {
            DraggableChordUI.Instantiate(chordPrefab, content, chord);
        }
    }
}