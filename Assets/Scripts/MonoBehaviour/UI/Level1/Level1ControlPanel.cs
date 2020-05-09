using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1 {
    public class Level1ControlPanel : MonoBehaviour {
        [SerializeField]
        private CustomUI.SelectableButton playButton;
        [SerializeField]
        private CustomUI.Button stopButton;

        public void SetPlaySelected(bool isSelected) {
            playButton.IsSelected = isSelected;
        }

        public CustomUI.SelectableButtonPressedEvent PlayButtonPressed => playButton.PressedSelectable;
        public CustomUI.ButtonPressedEvent StopButtonPressed => stopButton.Pressed;
    }
}
