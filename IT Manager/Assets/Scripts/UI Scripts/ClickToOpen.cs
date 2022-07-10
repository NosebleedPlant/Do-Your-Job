using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClickToOpen : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Image AssociatedFrame;
    [SerializeField]
    private Canvas DesktopCanvas;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("test");
        if(AssociatedFrame.enabled==false) AssociatedFrame.enabled=true;
        AssociatedFrame.transform.SetAsLastSibling();
    }
}
