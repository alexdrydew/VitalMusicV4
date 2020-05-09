using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Kars", menuName = "MidiPlayer/Kars")]
public class Kars : MidiPlayer {
    [DllImport("AudioPluginGuitarUnityx64")]
    private static extern void DistrhoKars_PlayNote(long sample, int note, int velocity, long sampleLength);

    [DllImport("AudioPluginGuitarUnityx64")]
    private static extern void DistrhoKars_AddEvent(long sample, int msg);
    [DllImport("AudioPluginGuitarUnityx64")]
    private static extern void DistrhoKars_MuteAll();

    public override void MuteAll() {
        DistrhoKars_MuteAll();
    }

    public void AddEvent(long sample, int msg) {
        DistrhoKars_AddEvent(sample, msg);
    }

    public override void PlayNote(int note, int velocity, long sampleLength, long sampleTime) {
        DistrhoKars_PlayNote(sampleTime, note, velocity, sampleLength);
    }
}