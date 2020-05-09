using UnityEngine;



namespace Managers {

    [CreateAssetMenu(fileName = "ApplicationManager", menuName = "Managers/ApplicationManager", order = 1)]
    public class ApplicationManager : ScriptableObject {
        [SerializeField]
        private AudioManager audioManager;
        [SerializeField]
        private LevelLoader loader;
        [SerializeField]
        private int startLevelIndex = 0;

        public AudioManager AudioManager => audioManager;

        private int currentLevelIndex;

        public void Init() {
            currentLevelIndex = startLevelIndex;
            loader.LoadLevel(startLevelIndex);
        }

        public void Destroy() {
            loader.Destroy();
        }

        public void NextLevel() {
            ++currentLevelIndex;
            loader.LoadLevel(currentLevelIndex);
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
