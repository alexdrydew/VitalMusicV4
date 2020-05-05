using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    protected LevelData data;

    protected CustomUI.LevelUI uiRoot;
    protected MusicSystem musicSystem;
    protected PlaySpace playSpace;

    protected virtual void Awake() {
        if (data == null) {
            throw new DataException<LevelLoader>();
        }
        uiRoot = Instantiate(data.LevelUI);
        musicSystem = Instantiate(data.LevelMusicSystem);
        playSpace = Instantiate(data.PlaySpace);

        uiRoot.Init(Camera.main, musicSystem, playSpace, data.LevelUIData);
        playSpace.Init(Camera.main, musicSystem, uiRoot, data.PlaySpaceData);
        musicSystem.Init(Camera.main, uiRoot, playSpace, data.MusicSystemData);
    }
}
