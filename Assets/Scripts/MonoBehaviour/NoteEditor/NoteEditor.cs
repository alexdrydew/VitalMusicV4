using System.Collections.Generic;
using System.Linq;
using ProgressGrid;
using UnityEngine;

namespace NoteEditor {
    public class NoteEditor : MusicEditor {
        private readonly List<NoteSelector> selectors = new List<NoteSelector>();

        [SerializeField]
        private NoteEditorData data;

        public NoteEditorUpdated NoteEditorUpdated { get; private set; }

        public List<Note> CurrentNotes {
            get { return selectors.Select(selector => selector.ActiveNote).ToList(); }
        }

        private void InitGrid(Vector2 cellSize) {
            grid.cellSize = cellSize;
            grid.transform.localPosition = Helpers.ReplaceY(
                grid.transform.localPosition, -cellSize.y / 2);
        }

        private void InitPointer() {
            pointer.Offset = new Vector2(
                0, grid.cellSize.y / 2 - pointer.GetComponent<SpriteRenderer>().bounds.size.y);
            pointer.Bounds = Bounds;
            MovePointerTo(0);
        }

        protected override void Awake() {
            base.Awake();

            if (NoteEditorUpdated == null) NoteEditorUpdated = new NoteEditorUpdated();

            Bounds = new BoundsInt(-slotsAmount / 2 - slotsAmount % 2, 0, 0, slotsAmount, 2, 0);

            float noteWidth = 0;

            Vector2? selectorSize = null;

            for (int i = Bounds.xMin; i < Bounds.xMax; ++i) {
                NoteSelector selector = Instantiate(data.NoteSelectorPrefab, grid.transform);
                if (selectorSize == null) {
                    selectorSize = selector.Size;
                    InitGrid(selector.Size);
                }

                selector.SelectorIndex = i - Bounds.xMin;
                selector.transform.localPosition = Helpers.ReplaceZ(
                    grid.GetCellCenterLocal(new Vector3Int(i, 0, 0)), -0.1f);
                selectors.Add(selector);
                selector.NoteSelected.AddListener(UpdateSelectors);
                noteWidth = selector.NoteSize.x;
            }

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.size = new Vector2(noteWidth * slotsAmount + 2 * data.ContentMargin,
                spriteRenderer.size.y);

            if (data.NoteProgressGridUiPrefab != null) {
                NoteProgressGridUi instance = Instantiate(data.NoteProgressGridUiPrefab, transform);
                instance.Grid = grid;
            }

            InitPointer();
        }

        private void UpdateSelectors(int selectorIndex) {
            void Update(int index) {
                if (index - 1 >= 0 && index + 1 < selectors.Count)
                    selectors[index].UpdateSelector(selectors[index - 1], selectors[index + 1]);
                else if (index - 1 >= 0)
                    selectors[index].UpdateSelector(selectors[index - 1], null);
                else if (index + 1 < selectors.Count)
                    selectors[index].UpdateSelector(null, selectors[index + 1]);
                else
                    selectors[index].UpdateSelector(null, null);
            }

            NoteEditorUpdated.Invoke();

            if (selectorIndex - 1 >= 0 && selectorIndex + 1 < selectors.Count) {
                selectors[selectorIndex].UpdateSelector(selectors[selectorIndex - 1], selectors[selectorIndex + 1]);
                Update(selectorIndex - 1);
                Update(selectorIndex + 1);
            }
            else if (selectorIndex - 1 >= 0) {
                selectors[selectorIndex].UpdateSelector(selectors[selectorIndex - 1], null);
                Update(selectorIndex - 1);
            }
            else if (selectorIndex + 1 < selectors.Count) {
                selectors[selectorIndex].UpdateSelector(null, selectors[selectorIndex + 1]);
                Update(selectorIndex + 1);
            }
            else
                selectors[selectorIndex].UpdateSelector(null, null);
        }
    }
}