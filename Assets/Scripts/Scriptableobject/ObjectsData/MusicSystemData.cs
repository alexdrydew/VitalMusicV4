using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicSystemData", menuName = "Objects data/MusicSystemData", order = 2)]
public class MusicSystemData : ScriptableObject {
    [SerializeField]
    private Tuple<int, int> timeSignature;
    [SerializeField]
    private int tempo;
    [SerializeField]
    private float blockLength;
    [SerializeField]
    private List<EventCell> grid;
    [SerializeField]
    private List<AudioClip> backingTracks;

    public Tuple<int, int> TimeSignature => timeSignature;

    public int Tempo => tempo;
    public float BlockLength => blockLength;
    public List<EventCell> Grid => grid;
    public List<AudioClip> BackingTracks => backingTracks;
}