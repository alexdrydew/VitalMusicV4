using UnityEngine;

[CreateAssetMenu(fileName = "Note", menuName = "Note")]
public class Note : ScriptableObject {
    [SerializeField]
    private NoteName noteName;

    [SerializeField]
    private int octave;

    public int Octave => octave;
    public NoteName NoteName => noteName;
    public int MidiNum => Helpers.GetMidiNum(noteName, octave);

    public static Note Create(NoteName noteName, int octave) {
        var instance = CreateInstance<Note>();
        instance.noteName = noteName;
        instance.octave = octave;
        return instance;
    }
}