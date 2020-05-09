using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tutorial {
    [CreateAssetMenu(fileName = "TutorialController", menuName = "Tutorial/TutorialController")]
    public class TutorialController : ScriptableObject {
        [SerializeField]
        private TutorialControllerData Data;
        [SerializeField]
        private CustomUI.MessageBoxUI uiPrefab;
        private CustomUI.MessageBoxUI msgBox;

        private int nodeIndex = 0;

        GameEvent.GameEvent lastEvent = null;

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
            if (nodeIndex < Data.nodes.Count) {
                NextNode();
            } else {
                msgBox.gameObject.SetActive(false);
                if (lastEvent != null) {
                    lastEvent.RemoveListener(Advance);
                }
            }
        }

        private void NextNode() {
            if (lastEvent != null) {
                lastEvent.RemoveListener(Advance);
            }
            msgBox.SetMessage(Data.nodes[nodeIndex].message);

            var gameEvent = Data.nodes[nodeIndex].eventToProceed;
            gameEvent.AddListener(Advance);
            lastEvent = gameEvent;
        }
    }
}

