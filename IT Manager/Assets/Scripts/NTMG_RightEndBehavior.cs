using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTMG_RightEndBehavior : MonoBehaviour
{
    [SerializeField] private NTMG_Wire_Behavior parent;
    [SerializeField] public int port;
    [SerializeField] public List<int> ip;

    private void Awake()
    {
        parent = GetComponentInParent<NTMG_Wire_Behavior>();
        port = parent.port;
        ip = parent.ip;

    }

}
