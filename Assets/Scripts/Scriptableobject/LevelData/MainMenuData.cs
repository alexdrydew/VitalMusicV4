using System.Collections;
using UnityEngine;

namespace Menu {
    [CreateAssetMenu(fileName = "MainMenu", menuName = "LevelData/MainMenu")]
    public class MainMenuData : LevelData {
        private AudioSource music;

        [SerializeField]
        private AudioSource musicPrefab;

        private GameObject ui;

        [SerializeField]
        private GameObject uiPrefab;

        private IEnumerator LoadAsync() {
            yield return LoadScene();
            LoadMenu();
            ToggleMenu();
            
            music = Instantiate(musicPrefab);
            music.Play();
        }

        public override void Load() {
            EntryPoint.Instance.StartCoroutine(LoadAsync());
        }

        public override void Unload() {
            base.Unload();
            Destroy(music);
        }
    }
}