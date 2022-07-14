using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network_ConnectionRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer Connection;
    [SerializeField] public int Port;
    private bool _connected = false;

    private void Awake()
    {
        Connection.SetPosition(0,Vector2.zero);
        Connection.SetPosition(1,Vector2.zero);
    }

    public void SetConnection(Vector2 end,bool _clear)
    {
        end = (_clear)? Vector2.zero : transform.InverseTransformPoint(end);
        if(!_connected)
        {
            Debug.Log("set");
            Connection.SetPosition(1,end);
        }
    }

    public void FreezConnection(Vector2 end)
    {
        Debug.Log("freeze");
        end = transform.InverseTransformPoint(end);
        Debug.Log(end);
        Connection.SetPosition(1,end);
        _connected = true;
    }
}
