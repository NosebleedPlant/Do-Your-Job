using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToOpen : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private GameObject AssociatedFrame;
    [SerializeField] private Canvas DesktopCanvas;
    [SerializeField] private float TweenTime;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(AssociatedFrame.activeSelf==false)
        {
            AssociatedFrame.SetActive(true);
            AssociatedFrame.transform.SetAsLastSibling();
        }
        else
        {
            AssociatedFrame.SetActive(false);
        }
    }
    
    public void Pop()
    {
        LeanTween.cancelAll();
        transform.localScale= new Vector3(0.5f,0.5f,0.5f);
        LeanTween.scale(this.gameObject,new Vector3(0.6f,0.6f,0.6f),TweenTime).setEasePunch();
    }
}
