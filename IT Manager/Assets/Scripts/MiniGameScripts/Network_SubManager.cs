using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Network_SubManager : MonoBehaviour
{
    [SerializeField] private List<int> _ipAddresses;
    [SerializeField] private List<int> _portNumbers;
    List<RaycastResult> click_results;
    private Vector3 _framePosition;
    private int _pairCount=4;
    private bool _makingConnection = false;

    [SerializeField]private Network_ConnectionRenderer _test;

    private void Awake()
    {
        dragWire = _DragWire;
        checkMaking = _CheckMaking;
        _framePosition = GameObject.Find("NetworkFrame").transform.position;
    }

    public Action<Vector3> dragWire;
    private void _DragWire(Vector3 position)
    {
        if(_makingConnection)
        {
            position += transform.position -_framePosition;
            _test.SetConnection(position);
        }
        else
        {
            _test.SetConnection(Vector2.zero);
        }

    }

    public Action<Vector3> checkMaking;
    private void _CheckMaking(Vector3 position)
    {
        position += transform.position -_framePosition;
        RaycastHit2D rayResults= Physics2D.Raycast(position,Vector2.down);
        if(rayResults.collider!=null && rayResults.transform.CompareTag("NTMG_LeftEnd"))
        {
            _makingConnection =  true;
        }
        else
        {
            _makingConnection = false;
        }
        Debug.Log(_makingConnection);
    }

    private void GeneratePortIpPairs()
    {
        Debug.Log("generate started");
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
}
