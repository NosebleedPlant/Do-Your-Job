using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTMG_Manager : MonoBehaviour
{
    [SerializeField] public List<int> ipAddresses;
    [SerializeField] public List<int> portNumbers;
    [SerializeField] public List<int> ipAddressesOriginal;
    [SerializeField] public List<int> portNumbersOriginal;
    [SerializeField] public int pairCount;
    [SerializeField] private int completionCounter;
    [SerializeField] private GameObject ui;
    [SerializeField] List<GameObject> wirePairs;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject wirePairPrefab;
    [SerializeField] private Vector3 wiresStartPosition;
    [SerializeField] private List<Vector3> positionList;
    // [SerializeField] private Canvas _canvas;

    [SerializeField] public List<int> leftIp;
    [SerializeField] public int leftPort;
    [SerializeField] public List<int> rightIp;
    [SerializeField] public int rightPort;
    [SerializeField] private bool drag = false;
    [SerializeField] Vector2 leftEndPosition;
    [SerializeField] Vector2 rightEndPosition;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        // _canvas = GetComponent<Canvas>();
        generatePortIpPairs();
        wiresStartPosition = new Vector3(-3.5f, 4f, 5f);
        // for (int i = 0, j = 0; i < 5; i++)
        // {
        //     positionList.Add(new Vector3(7f, j, -6f));
        //     Debug.Log(j);
        //     j -= 2;
        // }
    }

    private void Start()
    {
        // for (int i = 0; i < pairCount; i++)
        // {
        //     positionRandomizer.Add(i);
        // }
        ui = GameObject.Find("Pregame_UI");
    }

    public void startGame()
    {
        ui.transform.gameObject.SetActive(false);
        makePairs();
        randomizeRight();
        completionCounter = pairCount;
    }

    private void generatePortIpPairs()
    {
        Debug.Log("generate started");
        ipAddresses = new List<int>();
        portNumbers = new List<int>();

        for (int i = 0; i < pairCount; i++)
        {
            
            for (int j = 0; j < 4; j++)
            {
                ipAddresses.Add(Random.Range(1, 255));
                Debug.Log(ipAddresses[i*4 + j]);
            }
            portNumbers.Add(Random.Range(10, 1000));
        }
        ipAddressesOriginal = new List<int>();
        portNumbersOriginal = new List<int>();
        ipAddressesOriginal = ipAddresses;
        portNumbersOriginal = portNumbers;
    }

    private void makePairs()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        wirePairs.Clear();
        for (int i = 0; i < pairCount; i++)
        {
            wirePairs.Add(Instantiate(wirePairPrefab, wiresStartPosition, rotation));
            wirePairs[i].transform.SetParent(this.transform);
            wirePairs[i].transform.localPosition = Vector3.zero;
            wirePairs[i].transform.localPosition += wiresStartPosition;
            wiresStartPosition.y += -2;

            GameObject rightChild = wirePairs[i].transform.GetChild(1).gameObject;
            // GameObject leftChild = wirePairs[i].transform.GetChild(0).gameObject;

            // positionList.Add(transform.TransformPoint(rightChild.transform.localPosition));
            positionList.Add(rightChild.transform.position);
            // int randomPos = Random.Range(0, positionList.Count);
            // Vector3 temp = positionList[randomPos];
            // temp.y += wirePairs[i].transform.localPosition.y;
            // Debug.Log(temp.y + " " + wirePairs[i].transform.localPosition.y + " " + positionList[randomPos].y);
            // rightChild.transform.localPosition = temp;
            // positionList.RemoveAt(randomPos);
        }
    }

    private void randomizeRight()
    {
        // Debug.Log(positionList);
        for (int i = 0; i < pairCount; i++)
        {
            // NTMG_RightEndBehavior rightChild = wirePairs[i].GetComponent<NTMG_RightEndBehavior>();
            // int randomPos = positionRandomizer[Random.Range(0, positionRandomizer.Count)];
            // positionRandomizer.Remove(randomPos);
            // rightChild.changePosition(randomPos);
            GameObject rightChild = wirePairs[i].transform.GetChild(1).gameObject;
            int randomPos = Random.Range(0, positionList.Count);
            rightChild.transform.position = positionList[randomPos];
            positionList.RemoveAt(randomPos);
            // Vector2 oldPos = rightChild.transform.localPosition;
            
            // oldPos.y = positionList[Random.Range(0, randomPos)];
            // Debug.Log("PositionArrayCount: "+ positionList.Count + ", Old pos: " + positionList[Random.Range(0, randomPos)] + ", New pos: " + oldPos);
            // positionList.RemoveAt(randomPos);
            // rightChild.transform.position = oldPos; 
        }
    }

    public void leftEndClicked(GameObject leftNode, Transform position, List<int> ip, int port)
    {
        leftEndPosition = position.position;
        leftIp.Clear();
        leftIp = ip;
        leftPort = port;;
        drag = true;
    }

    public void rightEndClicked(GameObject rightNode, Transform position, List<int> ip, int port)
    {
        if (drag)
        {
            if (ip == leftIp && leftPort == port)
            {
                rightEndPosition = position.position;
                rightNode.GetComponent<NTMG_Wire_Behavior>().drawLine(leftEndPosition, rightEndPosition);
                completionCounter--;
                if (completionCounter <= 0)
                {
                    for (int i = pairCount - 1; i >= 0; i--)
                    {
                        Destroy(wirePairs[i]);
                        
                    }
                    wiresStartPosition = new Vector3(-3.5f, 4f, 5f);
                    positionList.Clear();
                    generatePortIpPairs();
                    //GameObject ui = GameObject.Find("Pregame_UI");
                    ui.transform.gameObject.SetActive(true);
                    GetComponentInChildren<NTMG_PregameUI>().UpdateText();
                    //GetComponent<NTMG_PregameUI>().UpdateText();
                }
            }
            else
            {
                resetLeft();
            }
        }

    }

    private void resetLeft()
    {
        // leftEndPosition = null;
        leftIp.Clear();
        leftPort = 0;
        drag = false;
    }
}
