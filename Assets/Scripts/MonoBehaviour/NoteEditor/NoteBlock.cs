using UnityEngine;
using UnityEngine.EventSystems;

namespace NoteEditor {
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class NoteBlock : MonoBehaviour, IPointerClickHandler {
        public enum Mode {
            Inactive,
            Left,
            Middle,
            Right,
            Single,
        }

        private Mode currentMode;

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

        public void OnPointerClick(PointerEventData eventData) {
            NoteSelected.Invoke(NotePos);
        }

        private void Awake() {
            if (NoteSelected == null) NoteSelected = new NoteSelectedEvent();
            spriteRenderer = GetComponent<SpriteRenderer>();
            CurrentMode = Mode.Inactive;
        }
    }
}