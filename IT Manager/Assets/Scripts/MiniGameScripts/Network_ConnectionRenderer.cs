using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Network_ConnectionRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer Connection;
    [SerializeField] public int Port;
    [SerializeField] private TextMeshProUGUI IPText;
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
            Connection.SetPosition(1,end);
        }
    }

    public void FreezConnection(Vector2 end)
    {
        end = transform.InverseTransformPoint(end);
        Connection.SetPosition(1,end);
        _connected = true;
    }

    public void SetIp(string ipAdress)
    {
        IPText.text = ipAdress;
    }
}
