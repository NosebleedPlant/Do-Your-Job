using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeSet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private GameStatusData gameStatusData;

    private DateTime starttime;
    private void OnEnable()
    {
        starttime = DateTime.Now;
    }
    private void Update()
    {
        float time = Time.time;
        gameStatusData.SurvivalTime = time.ToString("00000"); 
        timeDisplay.text = time.ToString("0000")+"secs \nSince Hire.";
    }
}
