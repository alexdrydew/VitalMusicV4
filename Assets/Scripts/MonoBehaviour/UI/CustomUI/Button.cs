﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CustomUI {
    [RequireComponent(typeof(Image))]
    public class Button : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField]
        private float hoverScaleFactor = 0.9f;

        [SerializeField]
        private float hoverScaleTime = 0.1f;

        [SerializeField]
        protected Sprite idleSprite;


        protected Image image;
        private Vector3 originalScale;


        public ButtonPressedEvent Pressed;

        protected RectTransform rectTransform;

        public virtual void OnPointerClick(PointerEventData eventData) {
            Pressed.Invoke();
        }

        public virtual void OnPointerEnter(PointerEventData eventData) {
            LeanTween.scale(rectTransform, originalScale * hoverScaleFactor, hoverScaleTime);
        }

        public virtual void OnPointerExit(PointerEventData eventData) {
            LeanTween.scale(rectTransform, originalScale, hoverScaleTime);
        }

        protected virtual void Awake() {
            image = GetComponent<Image>();
            image.sprite = idleSprite;

            if (Pressed == null) Pressed = new ButtonPressedEvent();

            rectTransform = GetComponent<RectTransform>();
            originalScale = rectTransform.localScale;
        }
    }
}