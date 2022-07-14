using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network_ConnectionRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer Connection;
    [SerializeField] public int Port;
    private const int ENDCOUNT = 2;
    public Vector2[] Ends = new Vector2[ENDCOUNT];
    private bool _connected = false;

    private void Awake()
    {
        Ends=new Vector2[]{Vector2.zero,Vector2.zero};
    }

    private void Update()
    {
        if(!_connected)
        {
            Connection.SetPosition(1,Ends[1]);
        }
    }

    public void SetConnection(Vector2 end)
    {
        end = transform.InverseTransformPoint(end);
        Ends[1] = end;
    }

    public void FreezConnection(Vector2 end)
    {
        Connection.SetPosition(1,end);
        Ends[1] = end;
        Connection.startColor = new Color(84f,220f,147f,0f);
        Connection.endColor = new Color(84f,220f,147f,0f);
        _connected = true;
    }
}
