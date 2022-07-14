using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Network_SubManager : MonoBehaviour
{
    [SerializeField] private Network_PortNumber PortPrefab;
    [SerializeField] private Network_ConnectionRenderer IpPrefab;
    [SerializeField] private Transform PrefabContainer;
    [SerializeField] private List<int> _ipAddresses;
    [SerializeField] private List<int> _portNumbers;
    [SerializeField] private Camera miniCam;
    private Vector2 _startingPositionPort= new Vector2(1.4f,0f);
    private Vector2 _startingPositionIP = new Vector2(-1.4f,0f);
    private Vector3 _framePosition;
    private int _pairCount=6;
    private bool _makingConnection = false;
    private Network_ConnectionRenderer _activeNode;

    private void Awake()
    {
        dragWire = _DragWire;
        checkMaking = _CheckMaking;
        _framePosition = GameObject.Find("NetworkFrame").transform.position;
        _startingPositionPort = PrefabContainer.TransformPoint(_startingPositionPort);
        _startingPositionIP = PrefabContainer.TransformPoint(_startingPositionIP);
        InstancePortIP_Pairs();
    }

    public Action<Vector3> dragWire;
    private void _DragWire(Vector3 position)
    {
        if(_makingConnection && _activeNode!=null)
        {
            position += transform.position -_framePosition;

            Collider2D overlap = Physics2D.OverlapPoint(position);
            if( overlap!=null
                &&_activeNode!=null
                &&overlap.transform.CompareTag("NTMG_RightEnd")
                &&_activeNode.Port== overlap.transform.GetComponent<Network_PortNumber>().portNumber)
            {
                _activeNode.FreezConnection(position);
                _activeNode=null;
            }
            else
            {
                _activeNode.SetConnection(position,false);
            }
            
        }
    }

    public Action<Vector3> checkMaking;
    private void _CheckMaking(Vector3 position)
    {
        position += transform.position -_framePosition;

        Collider2D overlap = Physics2D.OverlapPoint(position);
        if( overlap!=null
            &&overlap.transform.CompareTag("NTMG_LeftEnd"))
        {
            _makingConnection =  true;
            _activeNode = overlap.transform.GetComponent<Network_ConnectionRenderer>();
        }
    }

    public void ClearConnection()
    {
        _makingConnection=false;
        if(_activeNode!=null)
        {
            _activeNode.SetConnection(Vector2.negativeInfinity,true);
        }
    }

    private void GeneratePortIP_Pairs()
    {
        _ipAddresses.Clear();
        _portNumbers.Clear();

        for (int i = 0; i < _pairCount; i++)
        {
            
            for (int j = 0; j < 4; j++)
            {
                _ipAddresses.Add(UnityEngine.Random.Range(1, 255));
            }
            _portNumbers.Add(UnityEngine.Random.Range(10, 1000));
        }
    }


    private void InstancePortIP_Pairs()
    {
        Vector2 offsetPort = new Vector2(0f,0.9f);
        Vector2 offsetIP = new Vector2(0f,0.9f);
        // for (int i = 0; i < (_pairCount/2); i++)
        // {
        //     Vector2 positionPort = (_startingPositionPort + (offsetPort*i));
        //     Vector2 positionIP = (_startingPositionIP + (offsetIP*i));
        //     Network_PortNumber port = Instantiate(PortPrefab,positionPort,Quaternion.identity,PrefabContainer);
        //     Network_ConnectionRenderer ip = Instantiate(IpPrefab,positionIP,Quaternion.identity,PrefabContainer);
        //     ip.Port = i;
        //     port.portNumber = i;

        //     positionPort = (_startingPositionPort - (offsetPort*i));
        //     positionIP = (_startingPositionIP - (offsetIP*i));
        //     port = Instantiate(PortPrefab,positionPort,Quaternion.identity,PrefabContainer);
        //     ip = Instantiate(IpPrefab,positionIP,Quaternion.identity,PrefabContainer);
        //     ip.Port = i;
        //     port.portNumber = i;

        //     FindSpawn();
        // }

        for (int i = 0; i < (_pairCount); i++)
        {
            Vector2 position = FindSpawn();
            Network_PortNumber port = Instantiate(PortPrefab,position,Quaternion.identity,PrefabContainer);
            position = FindSpawn();
            Network_ConnectionRenderer ip = Instantiate(IpPrefab,position,Quaternion.identity,PrefabContainer);
            ip.Port = i;
            port.portNumber = i;
        }
    }

    private Vector2 FindSpawn()
    {
        //setup grid at runtime
        //randomly pick
        return Vector2.zero;
    }
}
