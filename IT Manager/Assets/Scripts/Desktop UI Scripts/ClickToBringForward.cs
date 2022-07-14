using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToBringForward : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]
    private GameObject Frame;
        
    public void OnPointerDown(PointerEventData eventData)
    {
        Frame.transform.SetAsLastSibling();
    }
}
