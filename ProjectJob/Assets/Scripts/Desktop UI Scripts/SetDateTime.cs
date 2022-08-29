using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SetDateTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DateTimeDisplay;
    void Update()
    {
        DateTimeDisplay.text = System.DateTime.Now.ToString("dd/MM/yyyy \nhh:mm tt");
    }
}
