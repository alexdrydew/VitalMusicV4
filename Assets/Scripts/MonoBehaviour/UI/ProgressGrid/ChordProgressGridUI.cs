using System.Collections.Generic;
using ChordEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressGrid {
    public class ChordProgressGridUI : ProgressGrid {
        [SerializeField]
        private ChordUI chordPrefab;

        private readonly List<ChordUI> chordUIs = new List<ChordUI>();

        [SerializeField]
        private Transform content;

        [SerializeField]
        private ScrollRect scrollRect;

        protected override void Awake() {
            base.Awake();

            if (chordPrefab == null) throw new DataException<ChordProgressGridUI>();

            foreach (ChordName chord in (data as ChordProgressGridData).HiddenChords) {
                ChordUI instance = Instantiate(chordPrefab, content);
                instance.Chord = ChordName.Hidden;
                chordUIs.Add(instance);
            }
        }

        private void Start() {
            scrollRect.horizontalNormalizedPosition = 0.0f;
        }

        public bool TryToRevealChord(int index, ChordName guessedChord) {
            if (!TryToRevealName(index, guessedChord)) return false;
            chordUIs[index].Chord = guessedChord;
            return true;
        }
    }
}