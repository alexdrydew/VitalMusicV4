using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class EditorPointer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static EditorPointer Instantiate(EditorPointer prefab, Transform parent,
        MusicEditor editor, Camera camera) {

        var instance = Instantiate(prefab, parent);
        instance.camera = camera;
        instance.editor = editor;
        instance.isProperlyInstantiated = true;
        return instance;
    }

    private MusicEditor editor;
    private bool isProperlyInstantiated = false;
    private Vector3Int cellPos;
    private Vector3Int dragStartCellPos;
    private new Camera camera;
    public Vector3Int CellPos { get => cellPos; set => cellPos = value; }

    private void Start() {
        if (!isProperlyInstantiated) {
            throw new InstantiationException<EditorPointer>();
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (editor.PointerInputBlocked) {
            return;
        }
        var localPointerPos = camera.ScreenToWorldPoint(eventData.position) 
            - editor.Grid.transform.position;
        Vector3Int targetCell = editor.Grid.LocalToCell(localPointerPos);
        if (targetCell.x != cellPos.x &&
            targetCell.x >= editor.Bounds.xMin && targetCell.x < editor.Bounds.xMax) {
            editor.MovePointerTo(new Vector3Int(targetCell.x, cellPos.y, 0));
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (cellPos != dragStartCellPos) {
            if ((editor as IMusicStartController) != null) {
                (editor as IMusicStartController).StartBlockChanged.Invoke(
                    cellPos.x - editor.Bounds.xMin);

                GlobalEventsManager.PointerMoved.Invoke();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        dragStartCellPos = cellPos;
    }
}
