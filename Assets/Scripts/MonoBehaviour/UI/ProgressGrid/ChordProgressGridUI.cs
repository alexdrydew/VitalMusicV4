using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressGrid {
    public class ChordProgressGridUI : ProgressGrid {
        [SerializeField]
        private ChordEditor.ChordUI chordPrefab;
        [SerializeField]
        private Transform content;
        [SerializeField]
        private ScrollRect scrollRect;

        private List<ChordEditor.ChordUI> chordUIs = new List<ChordEditor.ChordUI>();

        protected override void Awake() {
            base.Awake();

            if (chordPrefab == null) {
                throw new DataException<ChordProgressGridUI>();
            }

            foreach (ChordName chord in (data as ChordProgressGridData).HiddenChords) {
                var instance = Instantiate(chordPrefab, content);
                instance.Chord = ChordName.Hidden;
                chordUIs.Add(instance);
            }
        }

        private void Start() {
            scrollRect.horizontalNormalizedPosition = 0.0f;
        }

        public bool TryToRevealChord(int index, ChordName guessedChord) {
            if (base.TryToRevealName(index, guessedChord)) {
                chordUIs[index].Chord = guessedChord;
                return true;
            }
            return false;
        }
    }
}
