using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CustomUI {
    public class MessageBoxUI : MonoBehaviour, IPointerClickHandler {
        [SerializeField]
        private GameEvent.GameEvent messageBoxPressedEvent;
        [SerializeField]
        private TextMeshProUGUI text;

        public void SetMessage(string msg) {
            text.SetText(msg);
        }

        public void OnPointerClick(PointerEventData eventData) {
            messageBoxPressedEvent.Invoke();
        }
    }
}
