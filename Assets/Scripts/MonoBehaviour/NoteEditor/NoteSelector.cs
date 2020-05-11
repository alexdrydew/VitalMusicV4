using System.Collections.Generic;
using UnityEngine;

namespace NoteEditor {
    public class NoteSelector : MonoBehaviour {
        private readonly List<NoteBlock> blocks = new List<NoteBlock>();

        [SerializeField]
        private NoteBlock noteBlockPrefab;

        [SerializeField]
        private List<Note> notes;

        [HideInInspector]
        public int SelectorIndex;

        public int? ActiveBlockPos { get; private set; }

        public Note ActiveNote => ActiveBlockPos == null ? null : notes[(int) ActiveBlockPos];

        public NoteSelectorUpdatedEvent NoteSelected { get; private set; }
        public Vector2 NoteSize { get; private set; }

        public Vector2 Size => new Vector2(NoteSize.x, NoteSize.y * notes.Count);

        private void Awake() {
            if (NoteSelected == null) NoteSelected = new NoteSelectorUpdatedEvent();

            NoteSize = noteBlockPrefab.GetComponent<SpriteRenderer>().bounds.size;
            for (int i = -notes.Count / 2; i < notes.Count / 2 + notes.Count % 2; ++i) {
                NoteBlock block = Instantiate(noteBlockPrefab, transform);
                block.transform.localPosition = new Vector3(0, i * NoteSize.y, -0.1f);
                block.NotePos = i + notes.Count / 2;
                block.NoteSelected.AddListener(OnNoteSelected);
                blocks.Add(block);
            }
        }

        public void UpdateSelector(NoteSelector left, NoteSelector right) {
            bool isLeftConnected = false;
            if (left != null && left.ActiveBlockPos == ActiveBlockPos && ActiveBlockPos != null) isLeftConnected = true;
            bool isRightConnected = false;
            if (right != null && right.ActiveBlockPos == ActiveBlockPos && ActiveBlockPos != null)
                isRightConnected = true;
            if (isLeftConnected && isRightConnected)
                blocks[(int) ActiveBlockPos].CurrentMode = NoteBlock.Mode.Middle;
            else if (isLeftConnected)
                blocks[(int) ActiveBlockPos].CurrentMode = NoteBlock.Mode.Right;
            else if (isRightConnected)
                blocks[(int) ActiveBlockPos].CurrentMode = NoteBlock.Mode.Left;
            else if (ActiveBlockPos != null) blocks[(int) ActiveBlockPos].CurrentMode = NoteBlock.Mode.Single;
        }

        private void OnNoteSelected(int index) {
            if (blocks[index].CurrentMode == NoteBlock.Mode.Inactive) {
                if (ActiveBlockPos != null) blocks[(int) ActiveBlockPos].CurrentMode = NoteBlock.Mode.Inactive;
                ActiveBlockPos = index;
            }
            else {
                blocks[index].CurrentMode = NoteBlock.Mode.Inactive;
                ActiveBlockPos = null;
            }

            NoteSelected.Invoke(SelectorIndex);
        }
    }
}