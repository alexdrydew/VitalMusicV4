using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicSystemData", menuName = "Objects data/MusicSystemData", order = 2)]
public class MusicSystemData : ScriptableObject {
    public Tuple<int, int> TimeSignature;
    public int Tempo;
    public float BlockLength;
    public List<EventCell> Grid;
    public List<AudioClip> BackingTracks;
}