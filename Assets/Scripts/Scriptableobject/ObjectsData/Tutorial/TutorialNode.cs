using UnityEngine;

namespace Tutorial {
    [CreateAssetMenu(fileName = "TutorialNode", menuName = "Tutorial/TutorialNode")]
    public class TutorialNode : ScriptableObject {
        public GameEvent.GameEvent eventToProceed;
        public string message;
    }
}