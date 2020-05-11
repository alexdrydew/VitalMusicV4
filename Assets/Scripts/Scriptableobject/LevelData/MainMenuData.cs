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

        public override void Load() {
            if (ui != null) Destroy(ui);
            if (music != null) Destroy(music);
            ui = Instantiate(uiPrefab);
            music = Instantiate(musicPrefab);
            music.Play();
        }

        public override void Unload() {
            Destroy(ui);
            Destroy(music);
        }
    }
}