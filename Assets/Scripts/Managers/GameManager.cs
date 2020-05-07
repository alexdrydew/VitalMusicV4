using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    AudioManager audioManager;

    public static GameManager Instance { get; private set; }
    public AudioManager AudioManager => audioManager;

    private int currentSceneIndex = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this; 

        DontDestroyOnLoad(this.gameObject);
    }

    public void NextScene() {
        ++currentSceneIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }

    public void Quit() {
        Application.Quit();
    }
}
