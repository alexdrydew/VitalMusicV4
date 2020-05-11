using UnityEngine;

namespace CustomUI {
    public class ExtendedLevelControlPanel : LevelControlPanel {
        [SerializeField]
        private SelectableButton alternativePlayButton;

        public SelectableButtonPressedEvent AlternativePlayPressed =>
            alternativePlayButton.PressedSelectable;

        public void SetAlternativePlaySelected(bool isSelected) {
            alternativePlayButton.IsSelected = isSelected;
        }
    }
}