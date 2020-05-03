using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUI {
    public class ProgressGridUI : MonoBehaviour {
        [SerializeField]
        private ChordUI chordPrefab;
        [SerializeField]
        private List<ChordName> hiddenChords;

        private List<ChordUI> chords = new List<ChordUI>();
        private List<ChordName> realChords = new List<ChordName>();

        private void Init(List<ChordName> chordsToHide) {
            foreach (ChordName chord in chordsToHide) {
                var childChord = Instantiate(chordPrefab, transform);
                chords.Add(childChord);
                realChords.Add(chord);
                childChord.Chord = ChordName.Hidden;
            }
        }

        private void Awake() {
            if (chordPrefab == null) {
                throw new DataException<ProgressGridUI>();
            }

            Init(hiddenChords);
        }

        public void TryToRevealChord(int index, ChordName guessedChord) {
            if (realChords[index] == guessedChord) {
                chords[index].Chord = guessedChord;
            }
        }

        public bool CheckIfComplete() {
            for (int i = 0; i < chords.Count; ++i) {
                if (chords[i].Chord != realChords[i]) {
                    return false;
                }
            }
            return true;
        }
    }
}
