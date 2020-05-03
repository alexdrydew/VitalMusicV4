using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualNovelData", menuName = "Objects data/VisualNovelData", order = 0)]
public class VisualNovelData : ScriptableObject {
    [SerializeField]
    private List<VisualNovel.Scene> scenes;
    [SerializeField]
    private Sprite textBackground;
    [SerializeField]
    private VisualNovelUI uiPrefab;

    public List<VisualNovel.Scene> Scenes { get => scenes; }
    public Sprite TextBackground { get => textBackground; }
    public VisualNovelUI UiPrefab { get => uiPrefab; }

}