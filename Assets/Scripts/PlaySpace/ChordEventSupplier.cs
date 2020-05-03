using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordEventSupplier : MonoBehaviour, IMusicEventsSupplier
{
    [SerializeField]
    private int chordsVelocity = 60;
    [SerializeField]
    private int chordsPerBlock = 4;

    private MidiPlayer player;

    private List<MusicEvent> registeredEvents;
    private List<MusicEvent> pendingEvents;

    private MusicSystem musicSystem;
    public MusicSystem MusicSystem { get => musicSystem; set => musicSystem = value; }

    private void Awake() {
        player = PianoPlayer.Instance;
        pendingEvents = new List<MusicEvent>();
        registeredEvents = new List<MusicEvent>();
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
                AddChord(new Chord((ChordName) chords[i]), i);
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
