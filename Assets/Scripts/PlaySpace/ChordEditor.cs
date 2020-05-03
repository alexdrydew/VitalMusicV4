using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ChordEventSupplier), typeof(SpriteRenderer))]
public class ChordEditor : MusicEditor, IMusicStartController {
    [SerializeField]
    private ChordEditorData data;

    private ChordEventSupplier eventSupplier;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private PlaySpace playSpace;

    private List<ChordSlot> slots;
    private Vector2 chordSize;
    private Vector2 gridStart;

    private Vector3Int? savedPointerPos;

    private UnityEvent<int> startBlockChanged;
    public UnityEvent<int> StartBlockChanged { get => startBlockChanged; set => startBlockChanged = value; }

    protected override void Awake() {
        base.Awake();

        if (data == null) {
            throw new DataException<ChordEditor>();
        }

        if (StartBlockChanged == null) {
            StartBlockChanged = new EditorPointerPosChanged();
        }

        eventSupplier = GetComponent<ChordEventSupplier>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        var prefabSprite = data.ChordSlotPrefab.GetComponent<SpriteRenderer>();
        chordSize = prefabSprite.bounds.size;
    }

    private void Start() {
        int gridLength = eventSupplier.MusicSystem.Grid.Count;

        grid.cellSize = chordSize;
        grid.transform.localPosition = new Vector2(0, -chordSize.y / 2);

        Bounds = new BoundsInt(-gridLength / 2 - gridLength % 2, 0, 0, gridLength, 2, 0);

        slots = new List<ChordSlot>();

        for (int i = Bounds.xMin; i < Bounds.xMax; ++i) {
            slots.Add(ChordSlot.Instantiate(
                        data.ChordSlotPrefab, grid.transform,
                        grid.GetCellCenterLocal(new Vector3Int(i, 0, 0)),
                        playSpace.Camera, data.ChordPrefab, this
                    ));
        }

        spriteRenderer.size = new Vector2(chordSize.x * gridLength + 2 * data.ContentMargin,
            data.Height);

        pointer = EditorPointer.Instantiate(data.EditorPointerPrefab,
            grid.transform, this, playSpace.Camera);

        MovePointerTo(new Vector3Int(Bounds.xMin, -1, 0));

        playSpace.MusicSystem.BlockChanged.AddListener(
            (int pos) => MovePointerTo(new Vector3Int(pos + Bounds.xMin, -1, 0)));
        playSpace.MusicSystem.Started.AddListener(() => { SavePointerPos(); PointerInputBlocked = true; });
        playSpace.MusicSystem.Stopped.AddListener(() => { RestorePointerPos(); PointerInputBlocked = false; });
    }

    public void UpdateMusicEvents() {
        List<ChordName?> chords = new List<ChordName?>();
        for (int i = 0; i < slots.Count; ++i) {
            if (slots[i].Attached != null) {
                chords.Add(slots[i].Attached.Chord);
            } else {
                chords.Add(null);
            }
        }
        eventSupplier.UpdateChords(chords);
    }

    private void SavePointerPos() {
        savedPointerPos = pointer.CellPos;
    }

    private void RestorePointerPos() {
        if (savedPointerPos != null) {
            MovePointerTo((Vector3Int) savedPointerPos);
        }
        savedPointerPos = null;
    }

    public override void MovePointerTo(Vector3Int pos) {
        pointer.CellPos = pos;
        pointer.transform.localPosition = Helpers.AddZ(grid.GetCellCenterLocal(
            new Vector3Int(pos.x, pos.y, 0)), -0.1f);
    }
}
