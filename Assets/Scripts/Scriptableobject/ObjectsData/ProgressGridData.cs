using System;
using UnityEngine;

public abstract class ProgressGridData : ScriptableObject {
    public abstract bool CheckForEquality(int index, IComparable value);
    public abstract int GetNamesCount();
}