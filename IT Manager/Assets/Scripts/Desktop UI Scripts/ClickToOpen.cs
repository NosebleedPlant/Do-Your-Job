using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToOpen : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private GameObject AssociatedFrame;
    [SerializeField]
    private Canvas DesktopCanvas;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(AssociatedFrame.activeSelf==false) AssociatedFrame.SetActive(true);
        AssociatedFrame.transform.SetAsLastSibling();
    }
}
