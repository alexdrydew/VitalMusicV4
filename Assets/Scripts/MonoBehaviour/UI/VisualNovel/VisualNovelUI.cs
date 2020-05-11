using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VisualNovel {
    public class VisualNovelUI : MonoBehaviour, IPointerClickHandler {
        [SerializeField]
        private Image backgroundImage;

        public UnityEvent Skipped;

        [SerializeField]
        private TextMeshProUGUI speaker;

        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private Image textBackground;

        public Image TextBackground => textBackground;
        public TextMeshProUGUI Text => text;
        public Image BackgroundImage => backgroundImage;
        public TextMeshProUGUI Speaker => speaker;

        public void OnPointerClick(PointerEventData eventData) {
            Skipped.Invoke();
        }

        private void Awake() {
            if (Skipped == null) Skipped = new UnityEvent();
        }
    }
}