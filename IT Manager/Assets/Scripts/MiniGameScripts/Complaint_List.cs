using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complaint_List : MonoBehaviour
{
    [SerializeField] private RectTransform complaintPrefab;
    public List<RectTransform> complaints;
    private float _height;
    private float _start = 121.415f;
    void Start()
    {
        _height  = (GetComponent<RectTransform>().sizeDelta.y)/2;
    }

    public void InstanceComplaint()
    {
        RectTransform complaintObject = Instantiate(complaintPrefab,transform);
        complaintObject.localPosition = new Vector3(0,_start,0);
        complaintObject.localScale = Vector3.zero;
        complaints.Add(complaintObject);
        // tweenPosition(complaintObject);
        LeanTween.scale(complaintObject,new Vector3(0.5f,0.5f,0.5f),0.1f).setOnComplete(
            ()=>
            {
                TweenPosition(complaintObject);
            }
        );
    }

    public void DeleteComplaint()
    {
        // RectTransform complaintObject = complaints[0];
        if(complaints.Count==0){return;}
        
        LeanTween.scale(complaints[0].gameObject,Vector3.zero,0.1f).setOnComplete(
            ()=>
            {
                Destroy(complaints[0].gameObject);
                complaints.RemoveAt(0);
                foreach(RectTransform complaint in complaints)
                {
                    TweenPosition(complaint);
                }
            }
        );
    }

    private void TweenPosition(Transform obj)
    {
        float newPosition = -_height+ (((complaintPrefab.sizeDelta.y/2)+10)*(complaints.FindIndex(a=>a==obj)));
        LeanTween.moveLocal(obj.gameObject,new Vector3(0,newPosition,0),0.5f).setEaseOutBounce();
    }
}
