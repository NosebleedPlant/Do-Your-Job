using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network_ConnectionRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer Connection;
    private const int ENDCOUNT = 2;
    private Vector2[] _ends = new Vector2[ENDCOUNT];

    private void Awake()
    {
        _ends=new Vector2[]{Vector2.zero,Vector2.zero};
    }

    private void Update()
    {
        for(int i=0;i<ENDCOUNT;i++)
        {
            Connection.SetPosition(i,_ends[i]);
        }
    }

    public void SetConnection(Vector2 end)
    {
        end = transform.InverseTransformPoint(end);
        _ends[1] = end;
    }
}
