using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUI {
    public class ChordProgressGridUI : ProgressGrid {
        [SerializeField]
        private ChordUI chordPrefab;

        private List<ChordUI> chordUIs = new List<ChordUI>();

        protected override void Awake() {
            base.Awake();

            if (chordPrefab == null) {
                throw new DataException<ChordProgressGridUI>();
            }

            foreach (ChordName chord in (data as ChordProgressGridData).HiddenChords) {
                var instance = Instantiate(chordPrefab, transform);
                instance.Chord = ChordName.Hidden;
                chordUIs.Add(instance);
            }
        }

        public override bool TryToRevealName(int index, System.IComparable guessedName) {
            if (base.TryToRevealName(index, guessedName)) {
                chordUIs[index].Chord = (ChordName) guessedName;
                return true;
            }
            return false;
        }
    }
}
