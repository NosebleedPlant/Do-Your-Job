using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NTMG_PregameUI : MonoBehaviour
{
    [SerializeField] private NTMG_Manager manager;
    [SerializeField] private TMP_Text textRender;

    void Start()
    {
        manager = GetComponentInParent<NTMG_Manager>();
        UpdateText();
    }

    public void UpdateText()
    {
        textRender = GetComponent<TMP_Text>();
        string displayText = "     IP Addresses\t\tPort Numbers\n\r";
        for (int i = 0; i < manager.pairCount; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                displayText += (manager.ipAddressesOriginal[(i*4) + j] + ".");
            }
            displayText = displayText.Substring(0, displayText.Length - 1);
            displayText += "\t\t" + manager.portNumbersOriginal[i] + "\n\r";
        }

        textRender.text = displayText;
    }
}
