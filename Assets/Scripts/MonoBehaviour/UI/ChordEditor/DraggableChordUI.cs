using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace ChordEditor {
    public class DraggableChordUI : ChordUI, IBeginDragHandler, IDragHandler, IEndDragHandler,
        IPointerEnterHandler, IPointerExitHandler {

        public static DraggableChordUI Instantiate(DraggableChordUI prefab,
            Transform parent, ChordName chord) {
            var instance = Instantiate(prefab, parent);
            instance.Chord = chord;
            instance.isProperlyInstantiated = true;
            return instance;
        }

        private bool isProperlyInstantiated = false;

        [SerializeField]
        private DraggableChord prefab;

        [SerializeField]
        float hoverScaleFactor = 0.95f;
        [SerializeField]
        float hoverScaleTime = 0.1f;

        private DraggableChord currentDrag;
        private RectTransform rectTransform;
        private new Camera camera;

        Vector3 originalScale;

        protected override void Awake() {
            base.Awake();

            rectTransform = GetComponent<RectTransform>();
            originalScale = rectTransform.localScale;
            camera = Camera.main;
        }

        private void Start() {
            if (!isProperlyInstantiated) {
                throw new InstantiationException<DraggableChordUI>();
            }
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                currentDrag = DraggableChord.Instantiate(prefab, camera, Chord);
            }
        }

        public void OnDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                currentDrag.OnDrag(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                currentDrag.OnEndDrag(eventData);
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            LeanTween.scale(rectTransform, originalScale * hoverScaleFactor, hoverScaleTime);
        }

        public void OnPointerExit(PointerEventData eventData) {
            LeanTween.scale(rectTransform, originalScale, hoverScaleTime);
        }
    }
}

