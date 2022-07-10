using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickToClose : MonoBehaviour,IPointerClickHandler
{
    private Image _parentFrame;
    private void Start()
    {
        _parentFrame = transform.parent.gameObject.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _parentFrame.enabled = false;
        //replace with correct method call from application window
    }
}
