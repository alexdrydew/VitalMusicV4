using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "PianoPlayer", menuName = "MidiPlayer/PianoPlayer")]
public class PianoPlayer : MidiPlayer {
    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAPiano_PlayNote(long sample, int note, int velocity, long sampleLength);

    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAPiano_AddEvent(long sample, int msg);

    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAPiano_MuteAll();

    public override void PlayNote(int note, int velocity, long sampleLength, long sampleTime) {
        MDAPiano_PlayNote(sampleTime, note, velocity, sampleLength);
    }

    public override void MuteAll() {
        MDAPiano_MuteAll();
    }
}
