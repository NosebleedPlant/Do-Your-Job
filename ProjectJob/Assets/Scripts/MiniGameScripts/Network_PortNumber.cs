using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Network_PortNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PortText;
    public int portNumber;
    public bool connected=false;

    public void SetPort(string ipAdress)
    {
        PortText.text = ipAdress;
    }
}
