using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUI {
    abstract public class LevelUI : MonoBehaviour, IInitializedByLoader {
        protected Camera activeCamera;
        public Camera Camera { get => activeCamera; protected set => activeCamera = value; }
        abstract public bool CheckIfComplete();
        abstract public void Init(Camera camera, MusicSystem musicSystem);
    }

    public interface IHaveChordLibrary {
        ButtonPressedEvent ChordsLibraryButtonPressed { get; }
        RectTransform ChordsLibraryRoot { get; }
    }

    public interface IInteractWithMusicSystem {
        MusicSystem MusicSystem { get; }
    }

    public interface IHavePlayButton {
        SelectableButtonPressedEvent PlayButtonPressed { get; }
    }

    public interface IHaveStopButton {
        ButtonPressedEvent StopButtonPressed { get; }
    }
}

