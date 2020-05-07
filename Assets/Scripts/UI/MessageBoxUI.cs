using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace CustomUI {
    public class MessageBoxUI : MonoBehaviour, IPointerClickHandler {
        private TextMeshProUGUI text;

        private void Awake() {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetMessage(string msg) {
            text.SetText(msg);
        }

        public void OnPointerClick(PointerEventData eventData) {
            GlobalEventsManager.Invoke(GlobalEventType.MessageBoxPressed, this);
        }
    }
}
