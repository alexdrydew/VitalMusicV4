using UnityEngine;

namespace VisualNovel {
    [CreateAssetMenu(fileName = "VisualNovelController", menuName = "VisualNovel/VisualNovelController")]
    public class VisualNovelController : ScriptableObject {
        private int currentReplicaIndex;

        private int currentSceneIndex;
        private VisualNovelData data;
        private VisualNovelUI ui;

        public void Init(VisualNovelData data) {
            this.data = data;
            ui = Instantiate(data.UIPrefab);
            ui.Skipped.AddListener(Advance);

            currentSceneIndex = data.StartSceneIndex;
            currentReplicaIndex = 0;
            ui.BackgroundImage.sprite = getScene(currentSceneIndex).BackgroundImage;
            SetNextReplica();
        }

        public void Destroy() {
            Destroy(ui);
        }

        private void SetNextReplica() {
            Replica replica = data.Scenes[currentSceneIndex].Replicas[currentReplicaIndex++];
            ui.Speaker.SetText(replica.Who);
            ui.Text.SetText(replica.What);
        }

        private Scene getScene(int index) {
            if (currentSceneIndex >= data.Scenes.Count) return null;
            return data.Scenes[currentSceneIndex];
        }

        public void Advance() {
            Scene currentScene = getScene(currentSceneIndex);

            if (currentScene == null) return;
            if (currentReplicaIndex >= currentScene.Replicas.Count) {
                currentScene = getScene(++currentSceneIndex);
                if (currentScene == null) {
                    data.ApplicationManager.NextLevel();
                    return;
                }

                ui.BackgroundImage.sprite = currentScene.BackgroundImage;
                if (ui.BackgroundImage.sprite == null) ui.BackgroundImage.color = Color.black;
                currentReplicaIndex = 0;
            }

            SetNextReplica();
        }
    }
}