using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

namespace ChordEditor {
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChordEditor : MonoBehaviour {
        [SerializeField]
        private ChordEditorData data;
        [SerializeField]
        private int slotsAmount;
        [SerializeField]
        private Grid grid;
        [SerializeField]
        private EditorPointer pointer;

        private SpriteRenderer spriteRenderer;
        private BoundsInt bounds;

        public BoundsInt Bounds => bounds;

        private List<ChordSlot> slots;

        private Vector2 chordSize;
        private Vector2 gridStart;

        private Vector3Int? savedPointerPos;

        public EditorPointerPosChanged PointerPosChanged => pointer.PosChanged;
        public ChordAttachedToSlotEvent ChordsUpdated { get; private set; }

        private void Awake() {
            if (data == null) {
                throw new DataException<ChordEditor>();
            }

            if (ChordsUpdated == null) {
                ChordsUpdated = new ChordAttachedToSlotEvent();
            }

            spriteRenderer = GetComponent<SpriteRenderer>();

            var prefabSprite = data.ChordSlotPrefab.GetComponent<SpriteRenderer>();
            chordSize = prefabSprite.bounds.size;

            grid.cellSize = chordSize;
            grid.transform.localPosition = new Vector2(0, -chordSize.y / 2);

            bounds = new BoundsInt(-slotsAmount / 2 - slotsAmount % 2, 0, 0, slotsAmount, 2, 0);
            pointer.Bounds = bounds;

            InstantiateSlots();

            spriteRenderer.size = new Vector2(chordSize.x * slotsAmount + 2 * data.ContentMargin,
                spriteRenderer.size.y);
        }

        public List<ChordName?> CurrentNames {
            get {
                List<ChordName?> names = new List<ChordName?>();
                foreach (ChordSlot slot in slots) {
                    if (slot.Attached == null) {
                        names.Add(null);
                    } else {
                        names.Add(slot.Attached.Chord);
                    }
                }
                return names;
            }
        }

        private void InstantiateSlots() {
            if (ChordsUpdated == null) {
                ChordsUpdated = new ChordAttachedToSlotEvent();
            }

            slots = new List<ChordSlot>();

            for (int i = Bounds.xMin; i < Bounds.xMax; ++i) {
                var slot = ChordSlot.Instantiate(data.ChordSlotPrefab,
                    grid.transform,
                    grid.GetCellCenterLocal(new Vector3Int(i, 0, 0)),
                    data.ChordPrefab,
                    this);
                slot.ChordAttached.AddListener(ChordsUpdated.Invoke);
                slots.Add(slot);
            }
        }

        public void SavePointerPos() {
            savedPointerPos = pointer.CellPos;
            pointer.IsInputActive = false;
        }

        public void RestorePointerPos() {
            if (savedPointerPos != null) {
                pointer.MovePointerTo((Vector3Int)savedPointerPos);
            }
            savedPointerPos = null;
            pointer.IsInputActive = true;
        }

        public void MovePointerTo(int pos) {
            pointer.MovePointerXTo(pos + bounds.xMin);
        }
    }
}
