using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Complaint_SetText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Number;
    [SerializeField] private TextMeshProUGUI Complaint;

    private string[] Messages = 
    {
        "	 Hey my damn computer aint working.",
        "	 Please help.",
        "	 Hey I accidentally deleted that file.",
        "	 My computer is slow.",
        "	 Hey cant you help?",
        "	 Did you take care of the thing?",
        "	 YOU MESSED UP EVERYTHING!",
        "	 GET YOUR SHIT STRAIGHT!",
        "	 YOUR STUFF AINT DONE!"
    };

    public void Set(string number)
    {
        Number.text = "Ticket Number: "+number;
        Complaint.text = Messages[Random.Range(0,Messages.Length)];
    }
}
