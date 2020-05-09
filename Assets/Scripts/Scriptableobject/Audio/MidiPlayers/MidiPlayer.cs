using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Helpers {
    public static int GetMidiNum(NoteName note, int octave) {
        return (int)note + octave * 12;
    }
}

public abstract class MidiPlayer : ScriptableObject {
    public abstract void PlayNote(int note, int velocity, long sample_length, long sample_time);
    public abstract void MuteAll();
}