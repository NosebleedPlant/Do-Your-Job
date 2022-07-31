using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeSet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private GameStatusData gameStatusData;

    private float timer;
    private void Start()
    {
        Debug.Log("called");
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        gameStatusData.SurvivalTime = timer.ToString("00000"); 
        timeDisplay.text = gameStatusData.SurvivalTime +"secs \nSince Hire.";
    }
}
