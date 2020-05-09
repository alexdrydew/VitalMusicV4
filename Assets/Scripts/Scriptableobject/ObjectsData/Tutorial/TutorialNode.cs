using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial {

    [CreateAssetMenu(fileName = "TutorialNode", menuName = "Tutorial/TutorialNode")]
    public class TutorialNode : ScriptableObject {
        public string message;
        public GameEvent.GameEvent eventToProceed;
    }
}
