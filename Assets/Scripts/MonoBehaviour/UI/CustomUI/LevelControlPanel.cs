using UnityEngine;

namespace CustomUI {
    public class LevelControlPanel : MonoBehaviour {
        [SerializeField]
        private SelectableButton playButton;

        [SerializeField]
        private Button stopButton;

        public SelectableButtonPressedEvent PlayButtonPressed => playButton.PressedSelectable;
        public ButtonPressedEvent StopButtonPressed => stopButton.Pressed;

        public void SetPlaySelected(bool isSelected) {
            playButton.IsSelected = isSelected;
        }
    }
}