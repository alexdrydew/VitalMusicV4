using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public static partial class Helpers {
    public static int GetMidiNum(NoteName note, int octave) {
        return (int)note + octave * 12;
    }
}

public abstract class MidiPlayer {
    public abstract void PlayNote(int note, int velocity, long sample_length, long sample_time);
    public abstract void MuteAll();
}


public class PianoPlayer : MidiPlayer {
    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAPiano_PlayNote(long sample, int note, int velocity, long sampleLength);

    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAPiano_AddEvent(long sample, int msg);

    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAPiano_MuteAll();

    private static PianoPlayer instance;
    public static PianoPlayer Instance {
        get {
            if (instance == null) {
                return new PianoPlayer();
            } else {
                return instance;
            }
        }
    }

    private PianoPlayer() { }

    public override void PlayNote(int note, int velocity, long sampleLength, long sampleTime) {
        MDAPiano_PlayNote(sampleTime, note, velocity, sampleLength);
    }

    public override void MuteAll() {
        MDAPiano_MuteAll();
    }
}

public class EPianoPlayer : MidiPlayer {
    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAEPiano_PlayNote(long sample, int note, int velocity, long sampleLength);

    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAEPiano_AddEvent(long sample, int msg);
    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAEPiano_MuteAll();

    private static EPianoPlayer instance;
    public static EPianoPlayer Instance {
        get {
            if (instance == null) {
                return new EPianoPlayer();
            } else {
                return instance;
            }
        }
    }

    private EPianoPlayer() { }

    public override void PlayNote(int note, int velocity, long sampleLength, long sampleTime) {
        MDAEPiano_PlayNote(sampleTime, note, velocity, sampleLength);
    }

    public override void MuteAll() {
        MDAEPiano_MuteAll();
    }
}
