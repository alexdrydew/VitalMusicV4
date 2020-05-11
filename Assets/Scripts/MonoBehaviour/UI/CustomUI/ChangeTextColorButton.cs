using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CustomUI {
    public class ChangeTextColorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IPointerClickHandler {
        [SerializeField]
        private UnityEvent action;

        [SerializeField]
        private Color hoverColor;

        [SerializeField]
        private Color idleColor;

        private TextMeshProUGUI text;

        public void OnPointerClick(PointerEventData eventData) {
            action.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            text.color = hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData) {
            text.color = idleColor;
        }

        protected void Awake() {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable() {
            text.color = idleColor;
        }
    }
}