using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;

    private void Update()
    {
        float time = Time.time;
        timeDisplay.text = time.ToString("00:00")+"min \nSince Hire.";
    }
}
