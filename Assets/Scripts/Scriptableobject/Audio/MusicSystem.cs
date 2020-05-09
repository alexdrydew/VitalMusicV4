using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[System.Serializable]
public class Note {
    public readonly int Octave;
    public readonly NoteName Name;
    public readonly int MidiNum;

    public Note(NoteName name, int octave) {
        Name = name;
        Octave = octave;
        MidiNum = Helpers.GetMidiNum(name, octave);
    }
}

[System.Serializable]
public class Chord {
    private const int defaultOctave = 3;
    public readonly List<Note> Notes;
    public readonly ChordName Name;

    public Chord(params Note[] notes) {
        Notes = new List<Note>(notes);
        Name = ChordName.Custom;
    }

    public Chord(ChordName name) {
        if (name == ChordName.Hidden || name == ChordName.Custom) {
            throw new Exception("Invalid chord name");
        }
        Notes = new List<Note>();
        string nameStr = name.ToString();
        NoteName rootName = (NoteName)Enum.Parse(typeof(NoteName), nameStr.Substring(0, 1));
        Notes.Add(new Note(rootName, defaultOctave));
        Notes.Add(new Note(rootName + 7, defaultOctave)); // add fifth
        if (nameStr.Contains("m")) {
            Notes.Add(new Note(rootName + 3, defaultOctave));
        } else {
            Notes.Add(new Note(rootName + 4, defaultOctave));
        }
        Name = name;
    }
}

public enum ChordName {
    A, B, C, D, E, F, G, Bb, Db, Eb, Gb, Ab,
    Am, Bm, Cm, Dm, Em, Fm, Gm, Bbm, Dbm, Ebm, Gbm, Abm,
    Hidden, Custom
}

public enum NoteName {
    A = 21, B = 23, C = 24, D = 26, E = 28, F = 29, G = 31, Bb = 22, Db = 25, Eb = 27, Gb = 30, Ab = 32
}

[System.Serializable]
public class EventCell {
    public List<MusicEvent> events;
}

[System.Serializable]
public class MusicEvent {
    public delegate void EventAction(long sampleTime);
    public readonly EventAction Action;
    public int GridPos;

    public MusicEvent(EventAction action, int gridPos) {
        this.Action = action;
        this.GridPos = gridPos;
    }
}

namespace Managers {

    [CreateAssetMenu(fileName = "MusicSystem", menuName = "Managers/MusicSystem")]
    public class MusicSystem : ScriptableObject {
        [SerializeField]
        private MusicSystemData data;

        [SerializeField]
        private GameEvent.GameEvent playbackStartedEvent;
        [SerializeField]
        private AudioMixerGroup pianoMixerGroup;
        [SerializeField]
        private AudioMixerGroup ePianoMixerGroup;
        [SerializeField]
        private AudioMixerGroup backingTrackMixerGroup;

        private AudioSource pianoPlayer;
        private AudioSource ePianoPlayer;
        private AudioSource backingTrackPlayer;

        public List<EventCell> Grid { get => data.Grid; }

        public bool IsPlaying { get; private set; } = false;
        public int StartBlock { get; set; } = 0;

        public PerBlockEvent BlockChanged;
        public StartedEvent Started;
        public StoppedEvent Stopped;

        private long startSample;
        private int currentBlock;

        public void Init() {
            if (BlockChanged == null) {
                BlockChanged = new PerBlockEvent();
            }
            if (Started == null) {
                Started = new StartedEvent();
            }
            if (Stopped == null) {
                Stopped = new StoppedEvent();
            }

            var musicPlayers = Instantiate(new GameObject());
            StartBlock = 0;

            pianoPlayer = musicPlayers.AddComponent<AudioSource>();
            ePianoPlayer = musicPlayers.AddComponent<AudioSource>();
            backingTrackPlayer = musicPlayers.AddComponent<AudioSource>();

            pianoPlayer.outputAudioMixerGroup = pianoMixerGroup;
            ePianoPlayer.outputAudioMixerGroup = ePianoMixerGroup;
            backingTrackPlayer.outputAudioMixerGroup = backingTrackMixerGroup;

            if (data.BackingTracks != null && data.BackingTracks[0] != null) {
                backingTrackPlayer.clip = data.BackingTracks[0];
            }

            EntryPoint.Instance.Updated.AddListener(Update);

            ResetGrid();
        }

        public void Destroy() {
            if (pianoPlayer != null) {
                Destroy(pianoPlayer.gameObject);
            }

            EntryPoint.Instance.Updated.RemoveListener(Update);
        }

        public bool AddEvent(MusicEvent ev) {
            int pos = ev.GridPos;
            if (pos >= Grid.Count) {
                return false;
            }
            Grid[pos].events.Add(ev);
            return true;
        }

        private void ResetGrid() {
            foreach (var ev in Grid) {
                if (ev != null && ev.events != null) {
                    ev.events.Clear();
                }
            }
        }

        public bool RemoveEvent(MusicEvent ev) {
            int pos = ev.GridPos;
            if (pos >= Grid.Count) {
                return false;
            }
            return Grid[pos].events.Remove(ev);
        }

        public void PlayPause(bool pause) {
            if (pause) {
                Stop();
            } else {
                IsPlaying = true;
                pianoPlayer.Play();
                ePianoPlayer.Play();
                backingTrackPlayer.Play();
                backingTrackPlayer.time = (float)(StartBlock * GetBlockTimeLength());
                startSample = GetSampleTime();
                currentBlock = StartBlock;
                Started.Invoke();

                playbackStartedEvent.Invoke();

                AdvanceBlock(currentBlock);
            }
        }

        public void Stop() {
            IsPlaying = false;
            pianoPlayer.Stop();
            ePianoPlayer.Stop();
            backingTrackPlayer.Stop();
            MuteAll();
            Stopped.Invoke();
        }

        private void MuteAll() {
            //TODO
            //PianoPlayer.Instance.MuteAll();
            //EPianoPlayer.Instance.MuteAll();
        }

        private long GetSampleTime() {
            return (long)(AudioSettings.dspTime * AudioSettings.outputSampleRate);
        }

        public double GetBlockTimeLength() {
            double quarterTime = 60.0 / data.Tempo;
            double blockTime = quarterTime * 4 * data.BlockLength;
            return blockTime;
        }

        public long GetBlockSampleLength() {
            return (long)(AudioSettings.outputSampleRate * GetBlockTimeLength());
        }

        private void AdvanceBlock(int block) {
            ProcessEvents(block);
            BlockChanged.Invoke(block);
            ++currentBlock;
        }

        private void ProcessEvents(int block) {
            if (Grid[block].events != null) {
                foreach (var ev in Grid[block].events) {
                    ev.Action(GetBlockSampleLength() * (currentBlock - StartBlock) + startSample);
                }
            }
        }

        private void Update() {
            if (IsPlaying) {
                if (GetSampleTime() - startSample >= GetBlockSampleLength() * (currentBlock - StartBlock)) {
                    if (currentBlock >= Grid.Count) {
                        Stop();
                        return;
                    }
                    AdvanceBlock(currentBlock);
                }
            }
        }
    }
}
