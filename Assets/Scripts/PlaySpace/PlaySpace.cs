using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaySpace : MonoBehaviour, IInitializedByLoader {
    [SerializeField]
    private GameObject musicStartControllerGameObject;
    [SerializeField]
    private GameObject namesProviderGameObject;
    [SerializeField]
    private List<GameObject> musicEventsSuppliersGameObjects;
    private List<IMusicEventsSupplier> musicEventsSuppliers;

    public MusicSystem MusicSystem { get; private set; }
    public Camera Camera { get; private set; }
    public IMusicStartController MusicStartController { get; set; }

    UnityAction<int> checker;

    public void Init(Camera camera, MusicSystem musicSystem, CustomUI.LevelUI ui, PlaySpace playSpace) {
        MusicSystem = musicSystem;
        Camera = camera;
        foreach (var supplier in musicEventsSuppliers) {
            supplier.MusicSystem = musicSystem;
        }
        if ((ui as CustomUI.Level1UI) != null) {
            var chordsProvider = namesProviderGameObject.GetComponent<INamesProvider<ChordName>>();
            checker = (int i) => (ui as CustomUI.Level1UI).TryToRevealChord(i, chordsProvider.CurrentNames[i]);
        }
        MusicSystem.BlockChanged.AddListener(checker);
        MusicSystem.BlockChanged.AddListener((int i) => ui.TryToComplete());
    }

    private void Awake() {
        musicEventsSuppliers = new List<IMusicEventsSupplier>();
        foreach (GameObject go in musicEventsSuppliersGameObjects) {
            musicEventsSuppliers.Add(go.GetComponent<IMusicEventsSupplier>());
        }
        MusicStartController = musicStartControllerGameObject.GetComponent<IMusicStartController>();
    }
}
