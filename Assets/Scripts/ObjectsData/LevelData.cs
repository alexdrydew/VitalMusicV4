using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Objects data/Level Data", order = 1)]
public class LevelData : ScriptableObject {
    public CustomUI.LevelUI LevelUI;
    public MusicSystem LevelMusicSystem;
    public PlaySpace PlaySpace;
}