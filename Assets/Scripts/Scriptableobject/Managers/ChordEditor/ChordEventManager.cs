using System.Collections.Generic;
using UnityEngine;

namespace ChordEditor {
    [CreateAssetMenu(fileName = "ChordEventManager", menuName = "ChordEditor/ChordEventManager")]
    public class ChordEventManager : EventManager {
        [SerializeField]
        private int chordsPerBlock = 4;

        [SerializeField]
        private int chordsVelocity = 60;

        [SerializeField]
        private MidiPlayer player;

        public void UpdateChords(List<ChordName?> chords) {
            ResetEvents();
            for (int i = 0; i < chords.Count; ++i)
                if (chords[i] != null)
                    AddChord(new Chord((ChordName) chords[i]), i);
            UpdateEvents();
        }

        private void AddChord(Chord chord, int pos) {
            foreach (Note note in chord.Notes)
                for (int i = 0; i < chordsPerBlock; ++i) {
                    long offset = (long) (MusicSystem.GetBlockSampleLength() * ((double) i / chordsPerBlock));
                    var ev = new MusicEvent(
                        sample => player.PlayNote(
                            note.MidiNum, chordsVelocity, MusicSystem.GetBlockSampleLength(),
                            sample + offset),
                        pos);
                    pendingEvents.Add(ev);
                }
        }
    }
}