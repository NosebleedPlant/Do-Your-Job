using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToDelete : MonoBehaviour,IPointerClickHandler
{
    private GameObject _parentFrame;
    private void Start()
    {
        _parentFrame = transform.parent.gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("delete");
        Destroy(_parentFrame);
    }
}
