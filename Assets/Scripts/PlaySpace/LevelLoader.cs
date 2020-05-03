using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInitializedByLoader {
    void Init(Camera camera, MusicSystem musicSystem);
}

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private LevelData data;

    private CustomUI.LevelUI uiRoot;
    private MusicSystem musicSystem;
    private PlaySpace playSpace;

    private void Awake() {
        if (data == null) {
            throw new DataException<LevelLoader>();
        }
        uiRoot = Instantiate(data.LevelUI);
        musicSystem = Instantiate(data.LevelMusicSystem);
        playSpace = Instantiate(data.PlaySpace);

        uiRoot.Init(Camera.main, musicSystem);
        playSpace.Init(Camera.main, musicSystem);
        if (uiRoot as CustomUI.IHavePlayButton != null) {
            musicSystem.AssignPlay((uiRoot as CustomUI.IHavePlayButton).PlayButtonPressed);
        }
        if (uiRoot as CustomUI.IHaveStopButton != null) {
            musicSystem.AssignStop((uiRoot as CustomUI.IHaveStopButton).StopButtonPressed);
        }
        musicSystem.AssignStartControl(playSpace.MusicStartController.StartBlockChanged);
        
    }
}
