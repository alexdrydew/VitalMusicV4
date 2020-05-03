using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialData", menuName = "Objects data/TutorialData", order = 5)]
public class TutorialData : ScriptableObject
{
    [System.Serializable]
    public class TutorialNode {
        public string message;
        public Tutorial.TutorialActions actionToProceed;
    }

    public List<TutorialNode> nodes;
}
