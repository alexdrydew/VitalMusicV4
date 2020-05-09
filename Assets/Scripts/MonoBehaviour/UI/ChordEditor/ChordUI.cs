using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ChordEditor {
    public class ChordUI : MonoBehaviour {
        private TextMeshProUGUI text;
        private ChordName chord;
        public ChordName Chord {
            get => chord;
            set {
                this.chord = value;
                text.SetText(chord != ChordName.Hidden ? value.ToString() : "?");
            }
        }

        protected virtual void Awake() {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
