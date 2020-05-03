using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace CustomUI {
    public class DraggableChordUI : ChordUI, IBeginDragHandler, IDragHandler, IEndDragHandler {

        public static DraggableChordUI Instantiate(DraggableChordUI prefab,
            Transform parent, ChordName chord, LevelUI uiRoot) {
            var instance = Instantiate(prefab, parent);
            instance.uiRoot = uiRoot;
            instance.Chord = chord;
            instance.isProperlyInstantiated = true;
            return instance;
        }

        private LevelUI uiRoot;

        private bool isProperlyInstantiated = false;

        private RectTransform rectTransform;
        private Transform parent;


        protected override void Awake() {
            base.Awake();
            rectTransform = GetComponent<RectTransform>();
            parent = rectTransform.parent;
        }

        private void Start() {
            if (!isProperlyInstantiated) {
                throw new InstantiationException<ChordSlot>();
            }
        }

        public void OnBeginDrag(PointerEventData eventData) {

            if (eventData.button == PointerEventData.InputButton.Left) {
                transform.parent = (uiRoot as IHaveChordLibrary).ChordsLibraryRoot;
            }
        }

        public void OnDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                rectTransform.position = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                ContactFilter2D filter = new ContactFilter2D();
                filter.SetLayerMask(LayerMask.GetMask("Slot"));
                List<RaycastHit2D> results = new List<RaycastHit2D>();
                Physics2D.Raycast(uiRoot.Camera.ScreenToWorldPoint(eventData.position), Vector2.zero, filter, results);
                if (results.Count > 0) {
                    ChordSlot slot = results[0].transform.GetComponent<ChordSlot>();
                    if (slot != null) {
                        slot.AttachChord(Chord);
                    }
                }

                transform.parent = parent;
            }
        }
    }
}

