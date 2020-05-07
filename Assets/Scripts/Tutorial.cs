using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CustomUI.MessageBoxUI))]
public class Tutorial : MonoBehaviour {
    public TutorialData Data;
    private CustomUI.MessageBoxUI msgBox;

    private int nodeIndex = 0;

    GlobalEventType? lastEventType = null;

    private void Awake() {
        msgBox = GetComponent<CustomUI.MessageBoxUI>();
    }

    private void Start() {
        NextNode();
    }

    private void Advance(object sender) {
        ++nodeIndex;
        if (nodeIndex < Data.nodes.Count) {
            NextNode();
        } else {
            gameObject.SetActive(false);
            if (lastEventType != null) {
                GlobalEventsManager.RemoveListener(lastEventType.Value, Advance);
            }
        }
    }

    private void NextNode() {
        if (lastEventType != null) {
            GlobalEventsManager.RemoveListener(lastEventType.Value, Advance);
        }
        msgBox.SetMessage(Data.nodes[nodeIndex].message);

        var eventType = Data.nodes[nodeIndex].eventToProceed;
        GlobalEventsManager.AddListener(eventType, Advance);
        lastEventType = eventType;
    }



}
