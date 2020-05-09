using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace CustomUI {
    using Subscription = Tuple<Slider.SliderEvent, UnityAction<float>>;

    [System.Serializable]
    public class AudioSlider {
        [SerializeField]
        Slider slider;
        [SerializeField]
        List<string> audioVariables;

        public Slider Slider => slider;
        public List<string> AudioVariables => audioVariables;
    }

    public class AudioSettings : MonoBehaviour {
        [SerializeField]
        List<AudioSlider> sliders;
        [SerializeField]
        Managers.ApplicationManager applicationManager;

        private List<Subscription> subscriptions = new List<Subscription>();

        private void Awake() {
            subscriptions.Clear();

            foreach (AudioSlider audioSlider in sliders) {
                foreach (var param in audioSlider.AudioVariables) {
                    Subscription sub = new Subscription(
                        audioSlider.Slider.onValueChanged,
                        (float value) => applicationManager.AudioManager.SetFloat(param, value)
                    );
                    subscriptions.Add(sub);
                    sub.Item1.AddListener(sub.Item2);
                }
            }
        }

        private void OnDestroy() {
            foreach (Subscription sub in subscriptions) {
                if (sub.Item1 != null) {
                    sub.Item1.RemoveListener(sub.Item2);
                }
            }
        }
    }
}
