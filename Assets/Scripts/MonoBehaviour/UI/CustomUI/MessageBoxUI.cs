using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CustomUI {
    public class MessageBoxUI : MonoBehaviour, IPointerClickHandler {
        [SerializeField]
        private GameEvent.GameEvent messageBoxPressedEvent;

        [SerializeField]
        private TextMeshProUGUI text;

        public UnityEvent MessageBoxPressed { get; private set; }

        private void Awake() {
            if (MessageBoxPressed == null) {
                MessageBoxPressed = new UnityEvent();
            }
        }

        public void OnPointerClick(PointerEventData eventData) {
            messageBoxPressedEvent.Invoke();
            MessageBoxPressed.Invoke();
        }

        public void SetMessage(string msg) {
            text.SetText(msg);
        }
    }
}