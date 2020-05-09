using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelLoader", menuName = "Managers/LevelLoader")]
public class LevelLoader : ScriptableObject {
    [SerializeField]
    private List<LevelData> levels;
    private LevelData currentLevel;

    public void LoadLevel(int index) {
        LoadLevel(levels[index]);
    }

    public void LoadLevel(LevelData levelData) {
        if (currentLevel == levelData) {
            return;
        }
        if (currentLevel != null) {
            currentLevel.Unload();
        }
        levelData.Load();
        currentLevel = levelData;
    }

    public void Destroy() {
        if (currentLevel != null) {
            currentLevel.Unload();
            currentLevel = null;
        }
    }
}
