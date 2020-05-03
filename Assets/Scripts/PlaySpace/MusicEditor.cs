using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusicEditor : MonoBehaviour
{
    protected EditorPointer pointer;
    protected Grid grid;
    public Grid Grid => grid;
    public bool PointerInputBlocked { get; set; } = false;

    public BoundsInt Bounds { get => bounds; protected set => bounds = value; }

    private BoundsInt bounds;

    protected virtual void Awake() {
        grid = GetComponentInChildren<Grid>();
    }

    public abstract void MovePointerTo(Vector3Int pos);
}
