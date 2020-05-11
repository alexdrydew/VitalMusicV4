using UnityEngine;
using UnityEngine.EventSystems;

namespace CustomUI {
    public class SelectableButton : Button {
        [SerializeField]
        protected Sprite idleSelectedSprite;

        private bool isSelected;
        public SelectableButtonPressedEvent PressedSelectable;

        public bool IsSelected {
            get => isSelected;
            set {
                isSelected = value;
                UpdateSprite();
            }
        }

        protected override void Awake() {
            base.Awake();
            if (PressedSelectable == null) PressedSelectable = new SelectableButtonPressedEvent();
        }

        private void UpdateSprite() {
            if (IsSelected)
                image.sprite = idleSelectedSprite;
            else
                image.sprite = idleSprite;
        }

        public override void OnPointerClick(PointerEventData eventData) {
            IsSelected = !IsSelected;
            UpdateSprite();
            PressedSelectable.Invoke(!IsSelected);
        }
    }
}