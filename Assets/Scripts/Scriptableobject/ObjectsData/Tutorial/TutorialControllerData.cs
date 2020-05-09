using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial {
    [CreateAssetMenu(fileName = "TutorialControllerData", menuName = "Tutorial/TutorialControllerData")]
    public class TutorialControllerData : ScriptableObject {
        public List<TutorialNode> nodes;
    }
}
