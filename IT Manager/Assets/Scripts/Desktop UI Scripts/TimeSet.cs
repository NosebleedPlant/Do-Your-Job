using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private GameStatusData gameStatusData;

    private void Update()
    {
        float time = Time.time;
        gameStatusData.SurvivalTime = time.ToString("00:00"); 
        timeDisplay.text = time.ToString("0000")+"secs \nSince Hire.";
    }
}
