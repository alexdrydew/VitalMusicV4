using UnityEngine;

namespace Menu {

    [CreateAssetMenu(fileName = "MainMenu", menuName = "LevelData/MainMenu")]
    public class MainMenuData : LevelData {
        [SerializeField]
        private AudioSource musicPrefab;
        [SerializeField]
        private GameObject uiPrefab;

        private AudioSource music;
        private GameObject ui;

        public override void Load() {
            if (ui != null) {
                Destroy(ui);
            }
            if (music != null) {
                Destroy(music);
            }
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
