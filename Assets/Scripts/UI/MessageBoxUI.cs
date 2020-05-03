using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace CustomUI {
    public class MessageBoxUI : MonoBehaviour, IPointerClickHandler {
        private TextMeshProUGUI text;
        public ButtonPressedEvent Pressed { get; private set; }

        private void Awake() {
            text = GetComponentInChildren<TextMeshProUGUI>();
            if (Pressed == null) {
                Pressed = new ButtonPressedEvent();
            }
        }

        public void SetMessage(string msg) {
            text.SetText(msg);
        }

        public void OnPointerClick(PointerEventData eventData) {
            Pressed.Invoke();
        }
    }
}
