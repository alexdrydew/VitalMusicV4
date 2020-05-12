using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class EditorPointer : MonoBehaviour, IDragHandler {
    private new Camera camera;
    private Vector3Int cellPos;
    private Vector3Int dragStartCellPos;

    [SerializeField]
    private Grid grid;
    
    [SerializeField]
    private Vector2 offset;
    
    [SerializeField]
    private PointerEventData.InputButton dragButton;

    public BoundsInt Bounds { private get; set; }
    public bool IsInputActive { get; set; } = true;
    public EditorPointerPosChanged PosChanged { get; private set; }
    public Vector3Int CellPos => cellPos;

    [HideInInspector]
    public Vector2 Offset {
        get => offset;
        set => offset = value;
    }

    public void OnDrag(PointerEventData eventData) {
        if (!IsInputActive || eventData.button != PointerEventData.InputButton.Left) return;
        Vector3Int targetCell = grid.WorldToCell(camera.ScreenToWorldPoint(eventData.position));
        if (targetCell.x == cellPos.x || targetCell.x < Bounds.xMin || targetCell.x >= Bounds.xMax) return;
        PosChanged.Invoke(targetCell.x - Bounds.xMin);
        MovePointerTo(new Vector3Int(targetCell.x, cellPos.y, 0));
    }

    private void Awake() {
        camera = Camera.main;
        if (PosChanged == null) PosChanged = new EditorPointerPosChanged();
    }

    public void MovePointerTo(Vector3Int pos) {
        transform.localPosition = Helpers.ReplaceZ(
            (Vector2) grid.GetCellCenterLocal(pos) + Offset, transform.localPosition.z);
        cellPos = pos;
    }

    public void MovePointerXTo(int pos) {
        MovePointerTo(new Vector3Int(pos, cellPos.y, cellPos.z));
    }
}