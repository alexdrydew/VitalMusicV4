using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NoteEditor {
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class NoteBlock : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler {
        public enum Mode {
            Inactive,
            Left,
            Middle,
            Right,
            Single,
        }

        private Mode currentMode;
        private static Mode lastClickedNote;
        
        [SerializeField]
        private PointerEventData.InputButton inputButton;
        [SerializeField]
        private Sprite noteBlockActiveLeft;

        [SerializeField]
        private Sprite noteBlockActiveMiddle;

        [SerializeField]
        private Sprite noteBlockActiveRight;

        [SerializeField]
        private Sprite noteBlockActiveSingle;

        [SerializeField]
        private Sprite noteBlockInactive;

        [HideInInspector]
        public int NotePos;

        private SpriteRenderer spriteRenderer;

        private Vector2 spriteSize;
        
        public NoteSelectedEvent NoteSelected { get; private set; }

        public Mode CurrentMode {
            get => currentMode;
            set {
                switch (value) {
                    case Mode.Inactive:
                        spriteRenderer.sprite = noteBlockInactive;
                        break;
                    case Mode.Single:
                        spriteRenderer.sprite = noteBlockActiveSingle;
                        break;
                    case Mode.Left:
                        spriteRenderer.sprite = noteBlockActiveLeft;
                        break;
                    case Mode.Right:
                        spriteRenderer.sprite = noteBlockActiveRight;
                        break;
                    case Mode.Middle:
                        spriteRenderer.sprite = noteBlockActiveMiddle;
                        break;
                }

                currentMode = value;
            }
        }

        private void Awake() {
            if (NoteSelected == null) NoteSelected = new NoteSelectedEvent();
            spriteRenderer = GetComponent<SpriteRenderer>();
            CurrentMode = Mode.Inactive;
            spriteSize = spriteRenderer.bounds.size;
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (eventData.button != inputButton) return;
            lastClickedNote = CurrentMode;
            NoteSelected.Invoke(NotePos);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            if (eventData.pointerPress == null) return;
            if (((lastClickedNote == Mode.Inactive && CurrentMode == Mode.Inactive) ||
                 (lastClickedNote != Mode.Inactive && CurrentMode != Mode.Inactive)) &&
                Math.Abs(eventData.pointerPress.transform.localPosition.y - transform.localPosition.y) < spriteSize.y / 2) {
                NoteSelected.Invoke(NotePos);
            }
        }
    }
}