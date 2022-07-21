using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBounce : MonoBehaviour
{
    float boundX = 100,boundY= -236;
    Vector3 target;

    void Start()
    {
        Tween();
    }
    public void Tween()
    {
        target = new Vector3(Random.Range(-boundX,boundX),Random.Range(boundY,154),0);
        LeanTween.moveLocal(transform.gameObject,target,1).setOnComplete(Tween);
    }
}
