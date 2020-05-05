using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CustomUI.MessageBoxUI))]
public class Tutorial : MonoBehaviour {
    public enum TutorialActions {
        Read,
        PlaceChord,
        StartPlayback,
        MovePointer,
        OpenChordLibrary
    }

    public TutorialData Data;
    private CustomUI.MessageBoxUI msgBox;

    private int nodeIndex = 0;

    UnityEvent lastEvent;

    private void Awake() {
        msgBox = GetComponent<CustomUI.MessageBoxUI>();
    }

    private void Start() {
        NextNode();
    }

    private void Advance() {
        ++nodeIndex;
        if (nodeIndex < Data.nodes.Count) {
            NextNode();
        } else {
            gameObject.SetActive(false);
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
        switch (Data.nodes[nodeIndex].actionToProceed) {
            case TutorialActions.Read:
                msgBox.Pressed.AddListener(Advance);
                lastEvent = msgBox.Pressed;
                break;
            case TutorialActions.StartPlayback:
                GlobalEventsManager.PlaybackStarted.AddListener(Advance);
                lastEvent = GlobalEventsManager.PlaybackStarted;
                break;
            case TutorialActions.PlaceChord:
                GlobalEventsManager.ChordPlaced.AddListener(Advance);
                lastEvent = GlobalEventsManager.ChordPlaced;
                break;
            case TutorialActions.MovePointer:
                GlobalEventsManager.PointerMoved.AddListener(Advance);
                lastEvent = GlobalEventsManager.PointerMoved;
                break;
            case TutorialActions.OpenChordLibrary:
                GlobalEventsManager.ChordLibraryOpened.AddListener(Advance);
                lastEvent = GlobalEventsManager.ChordLibraryOpened;
                break;
        }
    }



}
