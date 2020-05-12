using UnityEngine;

public class MusicEditor : MonoBehaviour {
    [SerializeField]
    protected Grid grid;
    [SerializeField]
    protected EditorPointer pointer;
    [SerializeField]
    protected int slotsAmount;
    
    private Vector3Int? savedPointerPos;

    protected SpriteRenderer spriteRenderer;

    protected BoundsInt bounds;
    
    public EditorPointerPosChanged PointerPosChanged => pointer.PosChanged;

    protected virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void InitPointer(Vector3Int startCell) {
        pointer.Bounds = bounds;
        pointer.MovePointerTo(startCell);
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
        pointer.MovePointerXTo(pos + bounds.xMin);
    }
}