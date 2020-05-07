using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

namespace CustomUI {
    public class ChangeTextColorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

        [SerializeField]
        private Color hoverColor;
        [SerializeField]
        private Color idleColor;

        [SerializeField]
        private UnityEvent action;

        private TextMeshProUGUI text;

        protected void Awake() {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable() {
            text.color = idleColor;
        }

        public void OnPointerEnter(PointerEventData eventData) {
            text.color = hoverColor;
        }
        public void OnPointerExit(PointerEventData eventData) {
            text.color = idleColor;
        }

        public void OnPointerClick(PointerEventData eventData) {
            action.Invoke();
        }
    }
}
