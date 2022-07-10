using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTMG_Manager : MonoBehaviour
{
    [SerializeField] public List<int> ipAddresses;
    [SerializeField] public List<int> portNumbers;
    [SerializeField] private int pairCount;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private bool isDragStarted = false;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        generatePortIpPairs();
    }

    private void Update()
    {
        
    }

    private void generatePortIpPairs()
    {
        Debug.Log("this is called");
        ipAddresses.Clear();
        portNumbers.Clear();

        for (int i = 0; i < pairCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                ipAddresses.Add(Random.Range(1, 255));
            }
            portNumbers.Add(Random.Range(10, 1000));
        }
    }
}
