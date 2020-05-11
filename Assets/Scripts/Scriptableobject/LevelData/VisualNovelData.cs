using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VisualNovel {
    [CreateAssetMenu(fileName = "VisualNovelData", menuName = "VisualNovel/VisualNovelData")]
    public class VisualNovelData : LevelData {
        [SerializeField]
        private ApplicationManager applicationManager;

        [SerializeField]
        private VisualNovelController controller;

        [Scene] [SerializeField]
        private string scene;

        [SerializeField]
        private List<Scene> scenes;

        [SerializeField]
        private int startSceneIndex;

        [SerializeField]
        private Sprite textBackground;

        [SerializeField]
        private VisualNovelUI uiPrefab;

        public List<Scene> Scenes => scenes;
        public Sprite TextBackground => textBackground;
        public VisualNovelUI UIPrefab => uiPrefab;
        public ApplicationManager ApplicationManager => applicationManager;
        public int StartSceneIndex => startSceneIndex;

        private IEnumerator LoadAsync() {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
            while (!asyncLoad.isDone) yield return null;
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