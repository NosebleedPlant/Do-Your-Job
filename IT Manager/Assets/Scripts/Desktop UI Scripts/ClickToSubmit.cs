using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToSubmit : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private GameObject Response;
    private Complaint_ResponseFeild _responseScript;
    private void Start()
    {
        _responseScript = Response.GetComponent<Complaint_ResponseFeild>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        if(_responseScript.completed == true) _responseScript.Submit();
    }
}