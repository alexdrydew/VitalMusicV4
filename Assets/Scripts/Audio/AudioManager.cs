using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioManager {
    public enum Volume {
        Piano,
        String
    }

    [SerializeField]
    private AudioMixerGroup mixer;
    public AudioMixerGroup Mixer => mixer;

    public void SetFloat(string target, float value) {
        mixer.audioMixer.SetFloat(target, value);
    }
}
