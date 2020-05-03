using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class VisualNovelUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Image textBackground;
    [SerializeField]
    Image backgroundImage;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    TextMeshProUGUI speaker;

    public Image TextBackground { get => textBackground; }
    public TextMeshProUGUI Text { get => text; }
    public Image BackgroundImage { get => backgroundImage; }
    public TextMeshProUGUI Speaker { get => speaker; }

    public UnityEvent Skipped;

    public void OnPointerClick(PointerEventData eventData) {
        Skipped.Invoke();
    }

    private void Awake() {
        if (!textBackground || !text || !BackgroundImage) {
            throw new DataException<VisualNovelUI>();
        }

        if (Skipped == null) {
            Skipped = new UnityEvent();
        }
    }

    
}
