using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChordEditor {

    [CreateAssetMenu(fileName = "ChordEventManager", menuName = "ChordEditor/ChordEventManager")]
    public class ChordEventManager : ScriptableObject {
        [SerializeField]
        private int chordsVelocity = 60;
        [SerializeField]
        private int chordsPerBlock = 4;
        [SerializeField]
        private MidiPlayer player;

        private List<MusicEvent> registeredEvents = new List<MusicEvent>();
        private List<MusicEvent> pendingEvents = new List<MusicEvent>();

        private Managers.MusicSystem musicSystem;
        public Managers.MusicSystem MusicSystem => musicSystem;

        public void Init(Managers.MusicSystem musicSystem) {
            registeredEvents.Clear();
            pendingEvents.Clear();
            this.musicSystem = musicSystem;
        }

        public void ResetEvents() {
            foreach (var ev in registeredEvents) {
                musicSystem.RemoveEvent(ev);
            }
        }

        public void UpdateEvents() {
            foreach (var ev in pendingEvents) {
                if (!MusicSystem.AddEvent(ev)) {
                    throw new Exception("Failed adding chord");
                }
                registeredEvents.Add(ev);
            }
            pendingEvents.Clear();
        }

        public void UpdateChords(List<ChordName?> chords) {
            ResetEvents();
            for (int i = 0; i < chords.Count; ++i) {
                if (chords[i] != null) {
                    AddChord(new Chord((ChordName)chords[i]), i);
                }
            }
            UpdateEvents();
        }

        private void AddChord(Chord chord, int pos) {
            foreach (Note note in chord.Notes) {
                for (int i = 0; i < chordsPerBlock; ++i) {
                    long offset = (long)(musicSystem.GetBlockSampleLength() * ((double)i / chordsPerBlock));
                    MusicEvent ev = new MusicEvent(
                        (long sample) => player.PlayNote(
                            note.MidiNum, chordsVelocity, MusicSystem.GetBlockSampleLength(),
                            sample + offset),
                        pos);
                    pendingEvents.Add(ev);
                }
            }
        }
    }

}