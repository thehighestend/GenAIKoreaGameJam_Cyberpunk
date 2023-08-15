using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _text;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_text == null)
            _text = GetComponent<TextMeshProUGUI>();

        var linkIndex = TMP_TextUtilities.FindIntersectingLink(_text, eventData.position, null);
        if (linkIndex != -1)
        {
            var linkInfo = _text.textInfo.linkInfo[linkIndex];
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}
