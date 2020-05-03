using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySpace : MonoBehaviour, IInitializedByLoader {
    [SerializeField]
    private GameObject musicStartControllerGameObject;
    private IMusicStartController musicStartController;
    [SerializeField]
    private List<GameObject> musicEventsSuppliersGameObjects;
    private List<IMusicEventsSupplier> musicEventsSuppliers;

    private MusicSystem musicSystem;
    private Camera camera;
    public MusicSystem MusicSystem { get => musicSystem; private set => musicSystem = value; }
    public Camera Camera { get => camera; private set => camera = value; }
    public IMusicStartController MusicStartController { get => musicStartController; set => musicStartController = value; }

    public void Init(Camera camera, MusicSystem musicSystem) {
        MusicSystem = musicSystem;
        Camera = camera;
        foreach (var supplier in musicEventsSuppliers) {
            supplier.MusicSystem = musicSystem;
        }
    }

    private void Awake() {
        musicEventsSuppliers = new List<IMusicEventsSupplier>();
        foreach (GameObject go in musicEventsSuppliersGameObjects) {
            musicEventsSuppliers.Add(go.GetComponent<IMusicEventsSupplier>());
        }
        musicStartController = musicStartControllerGameObject.GetComponent<IMusicStartController>();
    }
}
