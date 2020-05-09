using UnityEngine;

abstract public class ProgressGridData : ScriptableObject {
    public abstract bool CheckForEquality(int index, System.IComparable value);
    public abstract int GetNamesCount();
}
