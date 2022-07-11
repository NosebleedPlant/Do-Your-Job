using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NTMG_Wire_Behavior : MonoBehaviour
{
    [SerializeField] public List<int> ip;
    [SerializeField] public int port;
    [SerializeField] private bool drag = false;
    [SerializeField] private bool leftClickACtive = false;
    [SerializeField] private GameObject NTMG_Manager;
    [SerializeField] private NTMG_Manager manager;

    [SerializeField] private List<int> currentIp;
    [SerializeField] private int targetPort;

    [SerializeField] Vector2 leftEndPosition;
    [SerializeField] Vector2 rightEndPosition;

    [SerializeField] private LineRenderer lr;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        lr.positionCount = 2;
        lr = GetComponent<LineRenderer>();
        NTMG_Manager = GameObject.Find("NTMG_Manager");
        manager = NTMG_Manager.GetComponent<NTMG_Manager>();
        getPortIp();
    }
    private void Update()
    {
        
    }

    private void getPortIp()
    {
        ip.Clear();
        if (manager.ipAddresses.Count > 0)
        for (int i = 0; i < 4; i++)
        {
            ip.Add(manager.ipAddresses[0]);
            manager.ipAddresses.RemoveAt(0);
        }
        port = manager.portNumbers[0];
        manager.portNumbers.RemoveAt(0);
    }

    public void leftEndClicked(Transform position, List<int> ip, int port)
    {
        manager.leftEndClicked(this.gameObject, position, ip, port);
        // leftEndPosition = position.position;
        // currentIp.Clear();
        // currentIp = ip;
        // targetPort = port;
        // drag = true;
    }

    public void rightEndClicked(Transform position, List<int> ip, int port)
    {
        manager.rightEndClicked(this.gameObject, position, ip, port);
        // if (drag)
        // {
        //     Debug.Log("right end clicked");
        //     if (ip == currentIp && targetPort == port)
        //     {
        //         rightEndPosition = position.position;
        //         lr.SetPosition(0, leftEndPosition);
        //         lr.SetPosition(1, rightEndPosition);
        //     }
        //     else
        //     {
        //         resetLeft();
        //     }
        // }

    }
    public void drawLine(Vector2 left, Vector2 right)
    {
        lr.SetPosition(0, left);
        lr.SetPosition(1, right);
    }
}
