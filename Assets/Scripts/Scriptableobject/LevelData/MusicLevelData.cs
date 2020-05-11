using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLevelData : LevelData {
    [SerializeField] [Scene]
    protected string levelScene;

    [SerializeField]
    protected MusicSystem musicSystem;

    protected virtual IEnumerator LoadAsync() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelScene);
        while (!asyncLoad.isDone) yield return null;
        musicSystem.Init();
    }

    public override void Load() {
        EntryPoint.Instance.StartCoroutine(LoadAsync());
    }

    public override void Unload() { }
}