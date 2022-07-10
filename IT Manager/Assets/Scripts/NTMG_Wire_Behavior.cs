using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTMG_Wire_Behavior : MonoBehaviour
{
    [SerializeField] private List<int> ip;
    [SerializeField] private int port;
    [SerializeField] private GameObject NTMG_Manager;
    [SerializeField] private NTMG_Manager manager;

    void Start()
    {
        manager = NTMG_Manager.GetComponent<NTMG_Manager>();
        getPortIp();
    }

    private void getPortIp()
    {
        ip.Clear();
        for (int i = 0; i < 4; i++)
        {
            ip.Add(manager.ipAddresses[0]);
            manager.ipAddresses.RemoveAt(0);
        }
        port = manager.portNumbers[0];
        manager.portNumbers.RemoveAt(0);
    }
}
