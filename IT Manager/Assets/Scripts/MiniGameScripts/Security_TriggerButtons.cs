using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_TriggerButtons : MonoBehaviour
{
    [SerializeField] private GameObject _sprite;
    private LayerMask _virusMask;
    private bool active = false;
    private Transform note;

    private void Awake()
    {
        _virusMask= LayerMask.GetMask("SCMG_Virus");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        active = true;
        note = other.transform;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        active = false;
        note = null;
    }

    void Activate()
    {
        if(active && note!=null)
        {
            LeanTween.scale(_sprite,new Vector3(0.6f,0.6f,0.6f),0.1f).setOnComplete
            (
                ()=>
                {
                    LeanTween.scale(_sprite,new Vector3(0.5f,0.5f,0.5f),0.1f);
                }
            );
            Destroy(note.gameObject);
        }
    }
}
