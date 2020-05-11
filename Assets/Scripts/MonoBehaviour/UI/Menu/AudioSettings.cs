using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CustomUI {
    using Subscription = Tuple<Slider.SliderEvent, UnityAction<float>>;

    [Serializable]
    public class AudioSlider {
        [SerializeField]
        private List<string> audioVariables;

        [SerializeField]
        private Slider slider;

        public Slider Slider => slider;
        public List<string> AudioVariables => audioVariables;
    }

    public class AudioSettings : MonoBehaviour {
        [SerializeField]
        private ApplicationManager applicationManager;

        [SerializeField]
        private List<AudioSlider> sliders;

        private readonly List<Subscription> subscriptions = new List<Subscription>();

        private void Awake() {
            subscriptions.Clear();

            foreach (AudioSlider audioSlider in sliders)
            foreach (string param in audioSlider.AudioVariables) {
                var sub = new Subscription(
                    audioSlider.Slider.onValueChanged,
                    value => applicationManager.AudioManager.SetFloat(param, value)
                );
                subscriptions.Add(sub);
                sub.Item1.AddListener(sub.Item2);
            }
        }

        private void OnDestroy() {
            foreach (Subscription sub in subscriptions)
                if (sub.Item1 != null)
                    sub.Item1.RemoveListener(sub.Item2);
        }
    }
}