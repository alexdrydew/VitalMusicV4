using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProgressGrid {
    public class NoteProgressGridUi : ProgressGrid {
        private readonly List<HiddenNoteUI> hiddenNotes = new List<HiddenNoteUI>();

        [SerializeField]
        private HiddenNoteUI hiddenNoteUiPrefab;

        public Grid Grid { get; set; }
        public Vector2 Offset { get; set; }
        public BoundsInt Bounds { get; set; }
        public Vector2 SpriteSize => hiddenNoteUiPrefab.GetComponent<SpriteRenderer>().bounds.size;

        private void Start() {
            if (Grid == null)
                throw new ApplicationException("Grid is not set");
            var noteData = (NoteProgressGridData) data;
            for (int i = Bounds.xMin; i < Bounds.xMax; ++i) {
                if (noteData.HiddenNotes[i - Bounds.xMin] != NoteName.None) {
                    HiddenNoteUI instance = Instantiate(hiddenNoteUiPrefab, Grid.transform);
                    instance.transform.localPosition = Helpers.ReplaceZ(
                        (Vector2) Grid.GetCellCenterLocal(new Vector3Int(
                            i, 1, 0)) + Offset, -0.1f);
                    instance.HiddenNote = noteData.HiddenNotes[i - Bounds.xMin];
                    hiddenNotes.Add(instance);
                } else {
                    hiddenNotes.Add(null);
                    guessedNames[i - Bounds.xMin] = true;
                }
            }
        }

        public bool TryToRevealNote(int index, NoteName note) {
            if (!TryToRevealName(index, note)) return false;
            hiddenNotes[index]?.Reveal();
            return true;
        }
    }
}