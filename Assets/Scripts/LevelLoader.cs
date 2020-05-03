using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInitializedByLoader {
    void Init(Camera camera, MusicSystem musicSystem, CustomUI.LevelUI ui, PlaySpace playSpace);
}

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

        uiRoot.Init(Camera.main, musicSystem, uiRoot, playSpace);
        playSpace.Init(Camera.main, musicSystem, uiRoot, playSpace);
        musicSystem.Init(Camera.main, musicSystem, uiRoot, playSpace);
    }
}
