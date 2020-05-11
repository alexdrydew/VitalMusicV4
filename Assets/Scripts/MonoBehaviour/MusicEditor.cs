using UnityEngine;

public class MusicEditor : MonoBehaviour {
    [SerializeField]
    protected Grid grid;

    [SerializeField]
    protected EditorPointer pointer;

    private Vector3Int? savedPointerPos;

    [SerializeField]
    protected int slotsAmount;

    protected SpriteRenderer spriteRenderer;

    public BoundsInt Bounds { get; protected set; }
    public EditorPointerPosChanged PointerPosChanged => pointer.PosChanged;

    protected virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SavePointerPos() {
        savedPointerPos = pointer.CellPos;
        pointer.IsInputActive = false;
    }

    public void RestorePointerPos() {
        if (savedPointerPos != null) pointer.MovePointerTo((Vector3Int) savedPointerPos);
        savedPointerPos = null;
        pointer.IsInputActive = true;
    }

    public void MovePointerTo(int pos) {
        pointer.MovePointerXTo(pos + Bounds.xMin);
    }
}