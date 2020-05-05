using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualNovel : MonoBehaviour {
    public enum Character {
        Willie,
        Narrator
    }

    public static readonly Dictionary<Character, string> CharactersNames = new Dictionary<Character, string> {
        [Character.Willie] = "<color=#3C8C78>Willie, the customer</color>",
        [Character.Narrator] = ""
    };

    [System.Serializable]
    public class Replica {
        public Character Character;
        public string Who { get => CharactersNames[Character]; }
        [TextArea(3, 10)]
        public string What = "";

    }

    [System.Serializable]
    public class Scene 
    {
        public Sprite BackgroundImage;
        public List<Replica> Replicas;
    }

    [SerializeField]
    private VisualNovelData data;

    private VisualNovelUI uiRoot;

    private int currentSceneIndex = 0;
    private int currentReplicaIndex = 0;

    public List<Scene> Scenes { get => data.Scenes; }

    // Prevent bug when copy-pasting text to scriptable object using editor
    private void CleanText() {
        foreach (var scene in Scenes) {
            foreach (var replica in scene.Replicas) {
                replica.What = replica.What.Replace("\r", "");
            }
        }
    }

    private void Awake() {
        if (data == null) {
            throw new DataException<VisualNovel>();
        }

        uiRoot = Instantiate(data.UiPrefab, transform);
        SetNextReplica();
        uiRoot.TextBackground.sprite = data.TextBackground;
        uiRoot.Skipped.AddListener(Advance);

        CleanText();
    }

    private void SetNextReplica() {
        Replica replica = Scenes[currentSceneIndex].Replicas[currentReplicaIndex++];
        uiRoot.Speaker.SetText(replica.Who);
        uiRoot.Text.SetText(replica.What);
    }

    private Scene getScene(int index) {
        if (currentSceneIndex >= Scenes.Count) {
            return null;
        }
        return Scenes[currentSceneIndex];
    }

    public void Advance() {
        Scene currentScene = getScene(currentSceneIndex);

        if (currentScene == null) {
            return;
        }
        if (currentReplicaIndex >= currentScene.Replicas.Count) {
            currentScene = getScene(++currentSceneIndex);
            if (currentScene == null) {
                GameManager.Instance.NextScene();
                return;
            }

            uiRoot.BackgroundImage.sprite = currentScene.BackgroundImage;
            currentReplicaIndex = 0;
        }
        SetNextReplica();
    }

}
