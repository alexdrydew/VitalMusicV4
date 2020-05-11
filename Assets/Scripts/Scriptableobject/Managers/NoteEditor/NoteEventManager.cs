using System.Collections.Generic;
using UnityEngine;

namespace NoteEditor {
    [CreateAssetMenu(fileName = "NoteEventManager", menuName = "NoteEditor/NoteEventManager")]
    public class NoteEventManager : EventManager {
        [SerializeField]
        private int notesVelocity = 60;

        [SerializeField]
        private MidiPlayer player;

        public void UpdateNotes(List<Note> notes) {
            ResetEvents();
            Note lastNote = null;
            int lastNoteLength = 0;
            int lastNotePos = 0;
            for (int i = 0; i < notes.Count; ++i)
                if (lastNote == null && notes[i] != null)
                    SetLastNote(notes[i], i);
                else if (lastNote != null && lastNote == notes[i])
                    ++lastNoteLength;
                else {
                    if (lastNote != null) AddNote(lastNote, lastNotePos, lastNoteLength);
                    if (notes[i] != null)
                        SetLastNote(notes[i], i);
                    else
                        lastNote = null;
                }

            if (lastNote != null) AddNote(lastNote, lastNotePos, lastNoteLength);
            UpdateEvents();

            void SetLastNote(Note newNote, int pos) {
                lastNote = newNote;
                lastNoteLength = 1;
                lastNotePos = pos;
            }
        }

        private void AddNote(Note note, int pos, int length) {
            var ev = new MusicEvent(
                sample => player.PlayNote(
                    note.MidiNum, notesVelocity, length * MusicSystem.GetBlockSampleLength(), sample),
                pos);
            pendingEvents.Add(ev);
        }
    }
}