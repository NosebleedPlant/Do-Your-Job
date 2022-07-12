using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTMG_WireDraggerBehavior : MonoBehaviour
{
    [SerializeField] public NTMG_Wire_Behavior wirePair;
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("NTMG_LeftEnd"))
        {
            wirePair = other.gameObject.GetComponentInParent<NTMG_Wire_Behavior>();
            wirePair.leftEndClicked(other.gameObject.transform, other.gameObject.GetComponent<NTMG_LeftEndBehavior>().ip, other.gameObject.GetComponent<NTMG_LeftEndBehavior>().port);
        }
        else if (other.CompareTag("NTMG_RightEnd"))
        {
            wirePair = other.gameObject.GetComponentInParent<NTMG_Wire_Behavior>();
            wirePair.rightEndClicked(other.gameObject.transform, other.gameObject.GetComponent<NTMG_RightEndBehavior>().ip, other.gameObject.GetComponent<NTMG_RightEndBehavior>().port);
        }
    }
}
