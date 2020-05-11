using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChordProgressGridData", menuName = "Objects data/ChordProgressGridData", order = 1)]
public class ChordProgressGridData : ProgressGridData {
    [SerializeField]
    private List<ChordName> hiddenChords;

    public List<ChordName> HiddenChords => hiddenChords;

    public override bool CheckForEquality(int index, IComparable value) {
        return (ChordName) value == HiddenChords[index];
    }

    public override int GetNamesCount() {
        return HiddenChords.Count;
    }
}