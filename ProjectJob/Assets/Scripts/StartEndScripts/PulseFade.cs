using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float start;
    [SerializeField] private float end;
    [SerializeField] private float time;
    void Start()
    {
        LeanTween.value(transform.gameObject,SetAlpha,start,end,time).setLoopPingPong();
    }

    private void SetAlpha(float value)
    {
        canvasGroup.alpha = value;
    }
}
