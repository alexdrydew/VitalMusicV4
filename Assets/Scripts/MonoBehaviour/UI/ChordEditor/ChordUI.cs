using TMPro;
using UnityEngine;

namespace ChordEditor {
    public class ChordUI : MonoBehaviour {
        private ChordName chord;
        private TextMeshProUGUI text;

        public ChordName Chord {
            get => chord;
            set {
                chord = value;
                text.SetText(chord != ChordName.Hidden ? value.ToString() : "?");
            }
        }

        protected virtual void Awake() {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}