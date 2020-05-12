using CustomUI;
using UnityEngine;

namespace Tutorial {
    [CreateAssetMenu(fileName = "TutorialController", menuName = "Tutorial/TutorialController")]
    public class TutorialController : ScriptableObject {
        [SerializeField]
        private TutorialControllerData data;

        private GameEvent.GameEvent lastEvent;
        private MessageBoxUI msgBox;

        private int nodeIndex;

        [SerializeField]
        private MessageBoxUI uiPrefab;

        public void Init() {
            nodeIndex = 0;

            msgBox = Instantiate(uiPrefab);
            NextNode();
        }

        public void Destroy() {
            Destroy(msgBox);
        }

        private void Advance() {
            ++nodeIndex;
            if (nodeIndex < data.nodes.Count)
                NextNode();
            else {
                msgBox.gameObject.SetActive(false);
                if (lastEvent != null) lastEvent.RemoveListener(Advance);
            }
        }

        private void NextNode() {
            if (lastEvent != null) lastEvent.RemoveListener(Advance);
            msgBox.SetMessage(data.nodes[nodeIndex].message);

            GameEvent.GameEvent gameEvent = data.nodes[nodeIndex].eventToProceed;
            gameEvent.AddListener(Advance);
            lastEvent = gameEvent;
        }
    }
}