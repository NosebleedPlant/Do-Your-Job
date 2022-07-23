using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBounce : MonoBehaviour
{
    [SerializeField] float boundX = 8,boundY= 11;
    [SerializeField] float speed = 0.3f;
    Vector3 target;

    void Start()
    {
        Tween();
    }
    public void Tween()
    {
        target = new Vector3(Random.Range(-boundX,boundX),Random.Range(-boundY,boundY),0);
        LeanTween.moveLocal(transform.gameObject,target,0.3f).setEaseLinear().setOnComplete(Tween);
    }
}
