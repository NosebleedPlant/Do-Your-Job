using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToBounce : MonoBehaviour
{
    [SerializeField] private float TweenTime;

    public void Pop()
    {
        LeanTween.cancel(transform.gameObject);
        transform.localScale= new Vector3(0.5f,0.5f,0.5f);
        LeanTween.scale(this.gameObject,new Vector3(0.6f,0.6f,0.6f),TweenTime).setEasePunch();
    }
}
