using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CustomUI {
    public class SelectableButton : CustomUI.Button {
        public SelectableButtonPressedEvent PressedSelectable;
        private bool isSelected = false;

        [SerializeField]
        protected Sprite idleSelectedSprite;

        public bool IsSelected {
            get => isSelected;
            set {
                isSelected = value;
                UpdateSprite();
            }
        }

        protected override void Awake() {
            base.Awake();
            if (PressedSelectable == null) {
                PressedSelectable = new SelectableButtonPressedEvent();
            }
        }

        private void UpdateSprite() {
            if (IsSelected) {
                image.sprite = idleSelectedSprite;
            } else {
                image.sprite = idleSprite;
            }
        }

        public override void OnPointerClick(PointerEventData eventData) {
            IsSelected = !IsSelected;
            UpdateSprite();
            PressedSelectable.Invoke(!IsSelected);
        }
    }
}
