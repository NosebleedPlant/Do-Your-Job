using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Network_SubManager : MonoBehaviour
{
    [SerializeField] private GameStatusData gameStatus;
    [SerializeField] private Network_PortNumber PortPrefab;
    [SerializeField] private Network_ConnectionRenderer IpPrefab;
    [SerializeField] private Transform PrefabContainer;
    [SerializeField] private GameObject PreGame;
    [SerializeField] private GameObject PostGame;
    [SerializeField] private TextMeshProUGUI IPdisplay;
    [SerializeField] private TextMeshProUGUI Portdisplay;
    [SerializeField] private GameObject Dressing;
    [SerializeField] private Transform MiniGameArea;
    [SerializeField] private GlitchEffect GlitchEffectTrigger;
    [SerializeField] private GameManager Manager;
    private AudioSource[] _ntmgSfx;
    private CanvasGroup PreGameGroup;
    private CanvasGroup PostGameGroup;
    private Network_ConnectionRenderer _activeNode;
    private Transform _frameTransform;
    private bool _makingConnection = false;
    private int _connected =0;

    private void Awake()
    {
        dragWire = _DragWire;
        checkMaking = _CheckMaking;
        _frameTransform = GameObject.Find("NetworkFrame").transform;
        PreGameGroup = PreGame.GetComponent<CanvasGroup>();
        PostGameGroup = PostGame.GetComponent<CanvasGroup>();
        _ntmgSfx = GetComponents<AudioSource>();
        PreGameReady();
    }

    //called on reset
    private void PreGameReady()
    {
        Debug.Log("called");
        _connected =0;
        PreGame.SetActive(true);
        PreGameGroup.interactable = true;
        PreGameGroup.blocksRaycasts = true;
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
        for (int i = 0; i < (gameStatus.NetworkGameData.PairCount); i++)
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

    //called on start button pressed
    public void OnReady()
    {
        PreGameGroup.interactable = false;
        PreGameGroup.blocksRaycasts = false;
        Dressing.SetActive(true);
        PreGame.SetActive(false);
        InstancePortIP_Pairs();
        Debug.Log("on ready");
        _ntmgSfx[0].Play();
    }

    //called on winscreen
    public void OnWin()
    {
        PostGame.SetActive(true);
        PostGameGroup.interactable = true;
        PostGameGroup.blocksRaycasts = true;
        
    }

    //called on winscreen button click
    public void OnReset()
    {
        PostGameGroup.interactable = false;
        PostGameGroup.blocksRaycasts = false;
        PostGame.SetActive(false);
        PreGameReady();
        _ntmgSfx[4].Play();
    }

    

    public Action<Vector3> dragWire;
    private void _DragWire(Vector3 position)
    {
        if(_frameTransform.GetSiblingIndex()!=MiniGameArea.childCount-1){return;}
        if(_makingConnection && _activeNode!=null)
        {
            position += transform.position -_frameTransform.position;

            Collider2D overlap = Physics2D.OverlapPoint(position);
            if(overlap!=null
                &&_activeNode!=null
                &&overlap.transform.CompareTag("NTMG_RightEnd"))
            {
                Network_PortNumber port = overlap.transform.GetComponent<Network_PortNumber>();
                
                if( _activeNode.Port== port.portNumber)
                {
                    _ntmgSfx[2].Play();
                    //decrement network counter
                    _connected++;
                    gameStatus.NetworkGameData.Current-=3;
                    _activeNode.FreezConnection(position);
                    port.connected = true;
                    _activeNode=null;
                    if(_connected>=gameStatus.NetworkGameData.PairCount)
                    {   gameStatus.NetworkGameData.Current-=5;
                        // StartCoroutine(win());
                        PreGameReady();
                        OnWin();
                    }
                }
                else if(!port.connected)
                {
                    _ntmgSfx[3].Play();
                    ClearConnection();
                    //raise reset flag here.
                    GlitchEffectTrigger.Trigger();
                    PreGameReady();
                }
            }
            else
            { _activeNode.SetConnection(position,false); }
            
        }
    }

    public Action<Vector3> checkMaking;
    private void _CheckMaking(Vector3 position)
    {
        if(_frameTransform.GetSiblingIndex()!=MiniGameArea.childCount-1){return;}
        position += transform.position -_frameTransform.position;

        Collider2D overlap = Physics2D.OverlapPoint(position);
        if( overlap!=null
            &&overlap.transform.CompareTag("NTMG_LeftEnd"))
        {
            _ntmgSfx[1].Play();
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
        
        for (int i = 0; i < gameStatus.NetworkGameData.PairCount; i++)
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
        for (int i = 0; i < (gameStatus.NetworkGameData.PairCount); i++)
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
        Manager.ResetNetworkFill();
        PreGameReady();
    }
    private void OnDisable() => StopAllCoroutines();
}
