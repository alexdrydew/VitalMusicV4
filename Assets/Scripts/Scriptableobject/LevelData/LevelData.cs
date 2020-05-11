using UnityEngine;

public abstract class LevelData : ScriptableObject {
    public abstract void Load();
    public abstract void Unload();
}