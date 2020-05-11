using System.Collections.Generic;
using UnityEngine;

namespace ChordEditor {
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChordEditor : MusicEditor {
        private Vector2 chordSize;

        [SerializeField]
        private ChordEditorData data;

        private Vector2 gridStart;

        private List<ChordSlot> slots;
        public ChordAttachedToSlotEvent ChordsUpdated { get; private set; }

        public List<ChordName?> CurrentNames {
            get {
                var names = new List<ChordName?>();
                foreach (ChordSlot slot in slots)
                    if (slot.Attached == null)
                        names.Add(null);
                    else
                        names.Add(slot.Attached.Chord);
                return names;
            }
        }

        protected override void Awake() {
            base.Awake();

            if (data == null) throw new DataException<ChordEditor>();

            if (ChordsUpdated == null) ChordsUpdated = new ChordAttachedToSlotEvent();

            var prefabSprite = data.ChordSlotPrefab.GetComponent<SpriteRenderer>();
            chordSize = prefabSprite.bounds.size;

            grid.cellSize = chordSize;
            grid.transform.localPosition = new Vector2(0, -chordSize.y / 2);

            Bounds = new BoundsInt(-slotsAmount / 2 - slotsAmount % 2, 0, 0, slotsAmount, 2, 0);
            pointer.Bounds = Bounds;

            InstantiateSlots();

            spriteRenderer.size = new Vector2(chordSize.x * slotsAmount + 2 * data.ContentMargin,
                spriteRenderer.size.y);
        }

        private void InstantiateSlots() {
            if (ChordsUpdated == null) ChordsUpdated = new ChordAttachedToSlotEvent();

            slots = new List<ChordSlot>();

            for (int i = Bounds.xMin; i < Bounds.xMax; ++i) {
                ChordSlot slot = ChordSlot.Instantiate(data.ChordSlotPrefab,
                    grid.transform,
                    grid.GetCellCenterLocal(new Vector3Int(i, 0, 0)),
                    data.ChordPrefab,
                    this);
                slot.ChordAttached.AddListener(ChordsUpdated.Invoke);
                slots.Add(slot);
            }
        }
    }
}