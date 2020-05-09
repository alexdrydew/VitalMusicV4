using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    Managers.ApplicationManager applicationManager;

    static public EntryPoint Instance { get; private set; }

    public UnityEvent Updated { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        if (Updated == null) {
            Updated = new UnityEvent();
        }

        DontDestroyOnLoad(this);
        applicationManager.Init();
    }

    private void Update() {
        Updated.Invoke();
    }

    private void OnDestroy() {
        applicationManager.Destroy();
    }
}
