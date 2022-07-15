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
    private Network_ConnectionRenderer _activeNode;
    private Vector3 _framePosition;
    private int _pairCount=6;
    private bool _makingConnection = false;

    private void Awake()
    {
        dragWire = _DragWire;
        checkMaking = _CheckMaking;
        _framePosition = GameObject.Find("NetworkFrame").transform.position;
        GenerateSpawnPoints();
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
    
    List<Vector2> spawnGrid = new List<Vector2>();

    public void GenerateSpawnPoints()
    {
        float y = transform.position.y-2.4f;
        for(int i=0;i<4;i++)
        {
            float x = transform.position.x-2.4f;
            y+=1f;
            for(int j=0;j<4;j++)
            {
                x+=1f;
                spawnGrid.Add(transform.InverseTransformDirection(new Vector2(x,y)));
            }
        }
    }
    
    public void InstancePortIP_Pairs()
    {

        for (int i = 0; i < (_pairCount); i++)
        {            
            Network_PortNumber port = Instantiate(PortPrefab,FindSpawn(),Quaternion.identity,PrefabContainer);
            Network_ConnectionRenderer ip = Instantiate(IpPrefab,FindSpawn(),Quaternion.identity,PrefabContainer);
            ip.Port = i;
            port.portNumber = i;
        }
    }

    public void GeneratePortIP_Pairs()
    {
        List<int> ipList = new List<int>();
        List<int> portList = new List<int>();
        
        for (int i = 0; i < _pairCount; i++)
        {
            
            for (int j = 0; j < 4; j++)
            {
                ipList.Add(UnityEngine.Random.Range(1, 255));
            }
            portList.Add(UnityEngine.Random.Range(10, 1000));
        }
    }

    private Vector2 FindSpawn()
    {
        int index = UnityEngine.Random.Range(0,spawnGrid.Count);
        Vector2 postion = spawnGrid[index];
        spawnGrid.RemoveAt(index);
        return postion;
    }    
}
