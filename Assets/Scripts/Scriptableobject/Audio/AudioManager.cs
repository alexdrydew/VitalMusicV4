using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers {
    [CreateAssetMenu(fileName = "AudioManager", menuName = "Managers/AudioManager")]
    public class AudioManager : ScriptableObject {
        public enum Volume {
            Piano,
            String
        }

        [SerializeField]
        private AudioMixer mixer;
        public AudioMixer Mixer => mixer;

        public void SetFloat(string target, float value) {
            mixer.SetFloat(target, value);
        }
    }
}
