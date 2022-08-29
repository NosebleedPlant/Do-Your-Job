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
    private AudioSource _clickSfx;
    void Start()
    {
        _clickSfx = GetComponentInParent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(AssociatedFrame.activeSelf==false) AssociatedFrame.SetActive(true);
        AssociatedFrame.transform.SetAsLastSibling();
        _clickSfx.Play();
    }
}
