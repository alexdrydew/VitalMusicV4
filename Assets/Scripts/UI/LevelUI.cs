using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUI {
    abstract public class LevelUI : MonoBehaviour {
        protected Camera activeCamera;
        public Camera Camera { get => activeCamera; protected set => activeCamera = value; }
        abstract public bool TryToComplete();
        abstract public void Init(Camera camera, MusicSystem musicSystem, PlaySpace playSpace, LevelUIData data);
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

