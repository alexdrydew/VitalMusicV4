using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualNovel {
    [CreateAssetMenu(fileName = "VisualNovelData", menuName = "VisualNovel/VisualNovelData")]
    public class VisualNovelData : LevelData {
        [Scene] [SerializeField]
        private string scene;
        [SerializeField]
        private List<Scene> scenes;
        [SerializeField]
        private Sprite textBackground;
        [SerializeField]
        private VisualNovelUI uiPrefab;
        [SerializeField]
        private VisualNovelController controller;
        [SerializeField]
        private Managers.ApplicationManager applicationManager;
        [SerializeField]
        private int startSceneIndex = 0;

        public List<Scene> Scenes => scenes;
        public Sprite TextBackground => textBackground;
        public VisualNovelUI UIPrefab => uiPrefab;
        public Managers.ApplicationManager ApplicationManager => applicationManager;
        public int StartSceneIndex => startSceneIndex;

        private IEnumerator LoadAsync() {
            var asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
            while (!asyncLoad.isDone) {
                yield return null;
            }
            controller.Init(this);
        }

        public override void Load() {
            EntryPoint.Instance.StartCoroutine(LoadAsync());
        }

        public override void Unload() {
            controller.Destroy();
        }
    }
}