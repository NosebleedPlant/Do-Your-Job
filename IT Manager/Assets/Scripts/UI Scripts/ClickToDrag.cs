using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToDrag : MonoBehaviour
{
    private RectTransform _parentFrame;
    private void Start()
    {
        _parentFrame = transform.parent.GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _parentFrame.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _parentFrame.anchoredPosition += eventData.delta;
    }
}
