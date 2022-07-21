using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float angle;
    public float speed;
    public LeanTweenType type;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAroundLocal(transform.gameObject,Vector3.forward,angle,speed).setLoopType(type);   
    }
}
