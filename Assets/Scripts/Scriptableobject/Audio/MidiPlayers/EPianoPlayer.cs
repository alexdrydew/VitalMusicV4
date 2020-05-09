using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "EPianoPlayer", menuName = "MidiPlayer/EPianoPlayer")]
public class EPianoPlayer : MidiPlayer {
    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAEPiano_PlayNote(long sample, int note, int velocity, long sampleLength);

    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAEPiano_AddEvent(long sample, int msg);
    [DllImport("AudioPluginMdaUnityPortx64")]
    private static extern void MDAEPiano_MuteAll();

    public override void PlayNote(int note, int velocity, long sampleLength, long sampleTime) {
        MDAEPiano_PlayNote(sampleTime, note, velocity, sampleLength);
    }

    public override void MuteAll() {
        MDAEPiano_MuteAll();
    }
}
