using UnityEngine;
using UnityEngine.EventSystems;

namespace ChordEditor {
    public class DraggableChordUI : ChordUI, IBeginDragHandler, IDragHandler, IEndDragHandler,
        IPointerEnterHandler, IPointerExitHandler {
        private new Camera camera;

        private DraggableChord currentDrag;

        [SerializeField]
        private float hoverScaleFactor = 0.95f;

        [SerializeField]
        private float hoverScaleTime = 0.1f;

        private bool isProperlyInstantiated;

        private Vector3 originalScale;

        [SerializeField]
        private DraggableChord prefab;

        private RectTransform rectTransform;

        public void OnBeginDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left)
                currentDrag = DraggableChord.Instantiate(prefab, camera, Chord);
        }

        public void OnDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) currentDrag.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) currentDrag.OnEndDrag(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            LeanTween.scale(rectTransform, originalScale * hoverScaleFactor, hoverScaleTime);
        }

        public void OnPointerExit(PointerEventData eventData) {
            LeanTween.scale(rectTransform, originalScale, hoverScaleTime);
        }

        public static DraggableChordUI Instantiate(DraggableChordUI prefab,
            Transform parent, ChordName chord) {
            DraggableChordUI instance = Instantiate(prefab, parent);
            instance.Chord = chord;
            instance.isProperlyInstantiated = true;
            return instance;
        }

        protected override void Awake() {
            base.Awake();

            rectTransform = GetComponent<RectTransform>();
            originalScale = rectTransform.localScale;
            camera = Camera.main;
        }

        private void Start() {
            if (!isProperlyInstantiated) throw new InstantiationException<DraggableChordUI>();
        }
    }
}