using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChordEditorData", menuName = "Objects data/ChordEditorData", order = 4)]
public class ChordEditorData : ScriptableObject {
    public DraggableChord ChordPrefab;
    public ChordSlot ChordSlotPrefab;
    public EditorPointer EditorPointerPrefab;
    public Vector3 Origin;
    public float ContentMargin;
    public float Height;
}
