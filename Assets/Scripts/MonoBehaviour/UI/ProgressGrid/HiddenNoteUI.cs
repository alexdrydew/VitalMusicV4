using TMPro;
using UnityEngine;

public class HiddenNoteUI : MonoBehaviour {
    [SerializeField]
    private TextMeshPro text;

    public NoteName HiddenNote { get; set; }

    private void Awake() {
        text.SetText("?");
    }

    public void Reveal() {
        text.SetText(HiddenNote.ToString());
    }
}