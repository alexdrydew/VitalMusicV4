using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ProgressGrid {
    [CreateAssetMenu(fileName = "NoteProgressGridData", menuName = "Objects data/NoteProgressGridData")]
    public class NoteProgressGridData : ProgressGridData {
        [SerializeField]
        private List<NoteName> hiddenNotes = Enumerable.Repeat(NoteName.None, 128).ToList();

        public List<NoteName> HiddenNotes => hiddenNotes;

        public override bool CheckForEquality(int index, IComparable value) {
            return HiddenNotes[index] == (NoteName) value;
        }

        public override int GetNamesCount() {
            return HiddenNotes.Count;
        }
    }
}