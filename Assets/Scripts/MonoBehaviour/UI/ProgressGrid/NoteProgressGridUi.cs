using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProgressGrid {
    public class NoteProgressGridUi : ProgressGrid {
        private readonly List<HiddenNoteUI> hiddenNotes = new List<HiddenNoteUI>();

        [SerializeField]
        private HiddenNoteUI hiddenNoteUiPrefab;

        public Grid Grid { get; set; }

        private void Start() {
            if (Grid == null)
                throw new ApplicationException("Grid is not set");
            var noteData = (NoteProgressGridData) data;
            for (int i = 0; i < noteData.HiddenNotes.Count; ++i) {
                if (noteData.HiddenNotes[i] == NoteName.None) continue;
                HiddenNoteUI instance = Instantiate(hiddenNoteUiPrefab, Grid.transform);
                instance.transform.localPosition = Helpers.ReplaceZ(
                    Grid.GetCellCenterLocal(new Vector3Int(
                        i - noteData.HiddenNotes.Count / 2, 1, 0)), -0.1f);
                instance.HiddenNote = noteData.HiddenNotes[i];
                hiddenNotes.Add(instance);
            }
        }

        public bool TryToRevealNote(int index, NoteName note) {
            if (!TryToRevealName(index, note)) return false;
            hiddenNotes[index].Reveal();
            return true;
        }
    }
}