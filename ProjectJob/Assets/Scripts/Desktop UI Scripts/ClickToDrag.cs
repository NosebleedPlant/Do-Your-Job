using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToDrag : MonoBehaviour,IDragHandler
{
    private RectTransform _parentFrame;
    private void Start()
    {
        _parentFrame = transform.parent.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _parentFrame.anchoredPosition += eventData.delta;
    }
}
