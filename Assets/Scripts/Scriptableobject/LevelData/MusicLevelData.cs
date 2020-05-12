using System.Collections;
using CustomUI;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MusicLevelData : LevelData {

    [SerializeField]
    private ApplicationManager applicationManager;
    
    [SerializeField]
    protected MusicSystem musicSystem;

    [SerializeField]
    protected CustomUI.MessageBoxUI endLevelScreenPrefab;

    protected CustomUI.MessageBoxUI endLevelScreen;
    
    
    protected void CompleteLevel() {
        endLevelScreen = Instantiate(endLevelScreenPrefab);
        endLevelScreen.MessageBoxPressed.AddListener(applicationManager.NextLevel);
    }
    
    public override void Unload() {
        base.Unload();
        Destroy(endLevelScreen);
        musicSystem.Destroy();
    }
}