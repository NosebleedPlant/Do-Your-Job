using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToClose : MonoBehaviour,IPointerClickHandler
{
    private GameObject _parentFrame;
    private AudioSource _clickSfx;
    private void Start()
    {
        _parentFrame = transform.parent.gameObject;
        _clickSfx = GetComponentInParent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _parentFrame.SetActive(false);
        _clickSfx.Play();
    }
}
