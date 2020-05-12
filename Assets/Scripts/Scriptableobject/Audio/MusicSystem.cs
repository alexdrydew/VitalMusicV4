using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Chord {
    private const int defaultOctave = 3;
    public readonly ChordName Name;
    public readonly List<Note> Notes;

    public Chord(params Note[] notes) {
        Notes = new List<Note>(notes);
        Name = ChordName.Custom;
    }

    public Chord(ChordName name) {
        if (name == ChordName.Hidden || name == ChordName.Custom) throw new Exception("Invalid chord name");
        Notes = new List<Note>();
        string nameStr = name.ToString();
        var rootName = (NoteName) Enum.Parse(typeof(NoteName), nameStr.Substring(0, 1));
        Notes.Add(Note.Create(rootName, defaultOctave));
        Notes.Add(Note.Create(rootName + 7, defaultOctave)); // add fifth
        if (nameStr.Contains("m"))
            Notes.Add(Note.Create(rootName + 3, defaultOctave));
        else
            Notes.Add(Note.Create(rootName + 4, defaultOctave));
        Name = name;
    }
}

public enum ChordName {
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    Bb,
    Db,
    Eb,
    Gb,
    Ab,
    Am,
    Bm,
    Cm,
    Dm,
    Em,
    Fm,
    Gm,
    Bbm,
    Dbm,
    Ebm,
    Gbm,
    Abm,
    Hidden,
    Custom,
}

public enum NoteName {
    C = 24,
    Db = 25,
    D = 26,
    Eb = 27,
    E = 28,
    F = 29,
    Gb = 30,
    G = 31,
    Ab = 32,
    A = 33,
    Bb = 34,
    B = 35,
    None = -1,
}

[Serializable]
public class EventCell {
    public List<MusicEvent> events;
}

[Serializable]
public class MusicEvent {
    public delegate void EventAction(long sampleTime);

    public MusicEvent(EventAction action, int gridPos) {
        Action = action;
        GridPos = gridPos;
    }

    public EventAction Action { get; }
    public int GridPos { get; }
}

namespace Managers {
    [CreateAssetMenu(fileName = "MusicSystem", menuName = "Managers/MusicSystem")]
    public class MusicSystem : ScriptableObject {
        [SerializeField]
        private AudioMixerGroup backingTrackMixerGroup;

        private List<AudioSource> backingTrackPlayers;
        private int currentBlock;
    
        [SerializeField]
        private MusicSystemData data;

        [SerializeField]
        private AudioMixerGroup ePianoMixerGroup;

        private AudioSource ePianoPlayer;

        [SerializeField]
        private List<MidiPlayer> midiPlayers;

        private GameObject musicPlayer;

        [SerializeField]
        private AudioMixerGroup pianoMixerGroup;

        private AudioSource pianoPlayer;

        [SerializeField]
        private GameEvent.GameEvent playbackStartedEvent;

        private long startSample;

        [SerializeField]
        private AudioMixerGroup stringMixerGroup;

        private AudioSource stringPlayer;

        public List<EventCell> Grid => data.Grid;

        public bool IsPlaying { get; private set; }
        public int StartBlock { get; set; }

        public PerBlockEvent BlockChanged { get; private set; }
        public StartedEvent Started { get; private set; }
        public StoppedEvent Stopped { get; private set; }

        public void Init() {
            if (BlockChanged == null) BlockChanged = new PerBlockEvent();
            if (Started == null) Started = new StartedEvent();
            if (Stopped == null) Stopped = new StoppedEvent();

            StartBlock = 0;
            var musicPlayerInstance = new GameObject {name = "AudioSource"};
            if (pianoMixerGroup != null) {
                pianoPlayer = musicPlayerInstance.AddComponent<AudioSource>();
                pianoPlayer.outputAudioMixerGroup = pianoMixerGroup;
            }

            if (ePianoMixerGroup != null) {
                ePianoPlayer = musicPlayerInstance.AddComponent<AudioSource>();
                ePianoPlayer.outputAudioMixerGroup = ePianoMixerGroup;
            }

            if (stringMixerGroup != null) {
                stringPlayer = musicPlayerInstance.AddComponent<AudioSource>();
                stringPlayer.outputAudioMixerGroup = stringMixerGroup;
            }

            if (backingTrackMixerGroup != null) {
                backingTrackPlayers = new List<AudioSource>();
                foreach (AudioClip clip in data.BackingTracks) {
                    var backingTrackPlayer = musicPlayerInstance.AddComponent<AudioSource>();
                    backingTrackPlayer.outputAudioMixerGroup = backingTrackMixerGroup;
                    backingTrackPlayer.clip = clip;
                    backingTrackPlayers.Add(backingTrackPlayer);
                }
            }

            EntryPoint.Instance.Updated.AddListener(Update);

            ResetGrid();
        }

        public void Destroy() {
            if (musicPlayer != null) {
                Destroy(musicPlayer);   
            }
            backingTrackPlayers = null;

            Started?.RemoveAllListeners();
            Stopped?.RemoveAllListeners();
            BlockChanged?.RemoveAllListeners();

            EntryPoint.Instance.Updated.RemoveListener(Update);
        }

        public bool AddEvent(MusicEvent ev) {
            int pos = ev.GridPos;
            if (pos >= Grid.Count) return false;
            Grid[pos].events.Add(ev);
            return true;
        }

        private void ResetGrid() {
            foreach (EventCell ev in Grid) ev?.events?.Clear();
        }

        public bool RemoveEvent(MusicEvent ev) {
            int pos = ev.GridPos;
            if (pos >= Grid.Count) return false;
            return Grid[pos].events.Remove(ev);
        }

        public void PlayPause(bool pause) {
            if (pause)
                Stop();
            else {
                IsPlaying = true;
                startSample = GetSampleTime();
                currentBlock = StartBlock;
                
                if (backingTrackPlayers != null) {
                    foreach (AudioSource player in backingTrackPlayers) {
                        player.Play();
                        player.time = (float) (StartBlock * GetBlockTimeLength());
                    }
                }
                
                Started.Invoke();

                if (playbackStartedEvent != null) playbackStartedEvent.Invoke();

                AdvanceBlock();
            }
        }

        public void Stop() {
            IsPlaying = false;
            if (pianoPlayer != null) pianoPlayer.Stop();
            if (ePianoPlayer != null) ePianoPlayer.Stop();
            if (stringPlayer != null) stringPlayer.Stop();
            if (backingTrackPlayers != null)
                foreach (AudioSource player in backingTrackPlayers)
                    player.Stop();
            MuteAll();
            Stopped.Invoke();
        }

        private void MuteAll() {
            foreach (MidiPlayer player in midiPlayers) player.MuteAll();
        }

        private long GetSampleTime() {
            if (backingTrackPlayers != null && backingTrackPlayers.Count > 0) {
                return (long) (AudioSettings.dspTime * backingTrackPlayers[0].clip.frequency);
            }
            return (long) (AudioSettings.dspTime * AudioSettings.outputSampleRate);
        }

        private double GetBlockTimeLength() {
            double quarterTime = 60.0 / data.Tempo;
            double blockTime = quarterTime * 4 * data.BlockLength;
            return blockTime;
        }

        public long GetOutputBlockSampleLength() {
            return (long) (GetBlockTimeLength() * AudioSettings.outputSampleRate);
        }
        
        public long GetBlockSampleLength() {
            if (backingTrackPlayers != null && backingTrackPlayers.Count > 0) {
                return (long) (GetBlockTimeLength() * backingTrackPlayers[0].clip.frequency);
            }

            return GetOutputBlockSampleLength();
        }

        private void AdvanceBlock() {
            ProcessEvents(currentBlock);
            BlockChanged.Invoke(currentBlock);
            ++currentBlock;
        }

        private void ProcessEvents(int block) {
            if (Grid[block].events != null)
                foreach (MusicEvent ev in Grid[block].events)
                    ev.Action((long) (AudioSettings.dspTime * AudioSettings.outputSampleRate));
        }

        private void Update() {
            if (!IsPlaying) return;
            if (GetSampleTime() - startSample < GetBlockSampleLength() * (currentBlock - StartBlock)) return;
            if (currentBlock >= Grid.Count) {
                Stop();
                return;
            }
            AdvanceBlock();
        }
    }
}