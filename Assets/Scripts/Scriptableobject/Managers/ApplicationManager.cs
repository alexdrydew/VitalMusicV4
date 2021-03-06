﻿using UnityEngine;

namespace Managers {
    [CreateAssetMenu(fileName = "ApplicationManager", menuName = "Managers/ApplicationManager", order = 1)]
    public class ApplicationManager : ScriptableObject {
        [SerializeField]
        private AudioManager audioManager;

        private int currentLevelIndex;

        [SerializeField]
        private LevelLoader loader;

        [SerializeField]
        private int startLevelIndex;

        public AudioManager AudioManager => audioManager;

        public void Init() {
            currentLevelIndex = startLevelIndex;
            loader.LoadLevel(startLevelIndex);
        }

        public void Destroy() {
            loader.Destroy();
        }

        public void RestartGame() {
            currentLevelIndex = 0;
            loader.LoadLevel(currentLevelIndex);
        }

        public void NextLevel() {
            ++currentLevelIndex;
            if (currentLevelIndex >= loader.LevelsCount) {
                currentLevelIndex = 0;
            }
            loader.LoadLevel(currentLevelIndex);
        }

        public void Quit() {
            Application.Quit();
        }
    }
}