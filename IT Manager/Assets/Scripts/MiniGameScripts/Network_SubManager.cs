using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
public class Network_SubManager : MonoBehaviour
{
    [SerializeField] private GameStatusData gameStatus;
    [SerializeField] private Network_PortNumber PortPrefab;
    [SerializeField] private Network_ConnectionRenderer IpPrefab;
    [SerializeField] private Transform PrefabContainer;
    [SerializeField] private GameObject PreGame;
    [SerializeField] private TextMeshProUGUI IPdisplay;
    [SerializeField] private TextMeshProUGUI Portdisplay;
    [SerializeField] private GameObject Dressing;
    private Network_ConnectionRenderer _activeNode;
    private Transform _frameTransform;
    private int _pairCount=3;//no more then 6 please
    private bool _makingConnection = false;
    private int _connected =0;

    private void Awake()
    {
        dragWire = _DragWire;
        checkMaking = _CheckMaking;
        _frameTransform = GameObject.Find("NetworkFrame").transform;
        PreGameReady();
    }

    private void PreGameReady()
    {
        _connected =0;
        PreGame.SetActive(true);
        Dressing.SetActive(false);
        GeneratePortIP_Pairs();
        GenerateSpawnPoints();
        foreach (Transform child in PrefabContainer) 
        {
            child.gameObject.SetActive(false);
            GameObject.Destroy(child.gameObject);
        }

        IPdisplay.text = "";
        Portdisplay.text = "";
        for (int i = 0; i < (_pairCount); i++)
        {            
            string displayText = "";
            for(int j = 0; j < 4; j++)
            {
                displayText += (ipList[(i*4) + j] + ".");
            }
            IPdisplay.text += displayText+("\n");
            Portdisplay.text += portList[i].ToString()+ "."+("\n");
        }
    }

    public void OnReady()
    {
        PreGame.SetActive(false);
        Dressing.SetActive(true);
        InstancePortIP_Pairs();
    }

    public Action<Vector3> dragWire;
    private void _DragWire(Vector3 position)
    {
        if(_frameTransform.GetSiblingIndex()!=4){return;}
        if(_makingConnection && _activeNode!=null)
        {
            position += transform.position -_frameTransform.position;

            Collider2D overlap = Physics2D.OverlapPoint(position);
            if( overlap!=null
                &&_activeNode!=null
                &&overlap.transform.CompareTag("NTMG_RightEnd")
                &&_activeNode.Port== overlap.transform.GetComponent<Network_PortNumber>().portNumber)
            {
                //decrement network counter
                _connected++;
                gameStatus.NetworkGameData.Current-=3;
                _activeNode.FreezConnection(position);
                _activeNode=null;
                if(_connected>=_pairCount)
                {   gameStatus.NetworkGameData.Current-=5;
                    // StartCoroutine(win());
                    PreGameReady();
                    }
            }
            else if(overlap!=null
                &&_activeNode!=null
                &&overlap.transform.CompareTag("NTMG_RightEnd"))
            {
                ClearConnection();
                //raise reset flag here.
                PreGameReady();
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
        if(_frameTransform.GetSiblingIndex()!=4){return;}
        position += transform.position -_frameTransform.position;

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
    
    List<int> ipList;
    List<int> portList;
    public void GeneratePortIP_Pairs()
    {
        ipList = new List<int>();
        portList = new List<int>();
        
        for (int i = 0; i < _pairCount; i++)
        {
            
            for (int j = 0; j < 4; j++)
            {
                ipList.Add(UnityEngine.Random.Range(1, 255));
            }
            portList.Add(UnityEngine.Random.Range(100, 1000));
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

            string displayText = "";
            for(int j = 0; j < 4; j++)
            {
                displayText += (ipList[(i*4) + j] + ".");
            }
            ip.SetIp(displayText);
            port.SetPort(portList[i].ToString()+ ".");
        }
    }

    List<Vector2> spawnGrid = new List<Vector2>();
    public void GenerateSpawnPoints()
    {
        spawnGrid = new List<Vector2>();
        float y = transform.position.y-2.95f;
        for(int i=0;i<5;i++)
        {
            float x = transform.position.x-3.3f;
            y+=1f;
            for(int j=0;j<3;j++)
            {
                x+=1.6f;
                spawnGrid.Add(transform.InverseTransformDirection(new Vector2(x,y)));
            }
        }
    }

    private Vector2 FindSpawn()
    {
        int index = UnityEngine.Random.Range(0,spawnGrid.Count);
        Vector2 postion = spawnGrid[index];
        spawnGrid.RemoveAt(index);
        return postion;
    }

    private IEnumerator win()
    {
        yield return new WaitForSeconds(1f);
        //add winscreen here
        PreGameReady();
    }
    private void OnDisable() => StopAllCoroutines();
}
