using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class LevelData : ScriptableObject {
    [Scene] [SerializeField]
    protected string unityScene;

    [SerializeField]
    protected GameObject menuPrefab;
    protected GameObject menu;

    protected virtual void LoadMenu() {
        menu = Instantiate(menuPrefab);
        menu.SetActive(false);
    }
    
    protected virtual IEnumerator LoadScene() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(unityScene);
        while (!asyncLoad.isDone) yield return null;
    }

    public abstract void Load();

    public virtual void Unload() {
        Destroy(menu);
    }

    protected void AssignMenu() {
        EntryPoint.Instance.EscapePressed.AddListener(ToggleMenu);
    }

    protected void RemoveMenuAssignment() {
        EntryPoint.Instance.EscapePressed.RemoveListener(ToggleMenu);
    }
    
    protected void ToggleMenu() {
        menu.SetActive(!menu.activeSelf);
    }
}