using Managers;
using UnityEngine;
using UnityEngine.Events;

public class EntryPoint : MonoBehaviour {
    [SerializeField]
    private ApplicationManager applicationManager;

    public static EntryPoint Instance { get; private set; }

    public UnityEvent Updated { get; private set; }
    public UnityEvent EscapePressed { get; private set; }

    private void Awake() {
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;

        if (Updated == null) Updated = new UnityEvent();
        if (EscapePressed == null) EscapePressed = new UnityEvent();

        DontDestroyOnLoad(this);
        applicationManager.Init();
    }

    private void Update() {
        Updated.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            EscapePressed.Invoke();
        }
    }

    private void OnDestroy() {
        applicationManager.Destroy();
    }
}