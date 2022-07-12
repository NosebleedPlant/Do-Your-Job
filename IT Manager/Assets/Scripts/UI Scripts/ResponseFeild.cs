using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseFeild : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField InputField;
    [SerializeField]
    private Image ProgressBar;
    [SerializeField]
    private GameObject MinigameManager;
    private TicketMiniGame _minigameManagerScript;
    private static string _message = "Hello, this is XXXXX. I have resolved the issue you were facing. I thank you for you patience and apologize for the inconvinence";
    private static int maxLength = _message.Length;
    public bool completed = false;

    private void Awake()
    {
        _minigameManagerScript = MinigameManager.GetComponent<TicketMiniGame>();
        InputField.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(string text)
    {
        int length = text.Length;
        completed = (maxLength==length)?true:false;
        if(length>0 && length<=maxLength)
        {
            //replace text
            string sub = _message.Substring(0, length);
            InputField.text = sub;
            //update progress
            ProgressBar.fillAmount = (float)length/maxLength;
        }
        else if(length>maxLength)
        {
            float test = 2-(float)length/maxLength;
            Debug.Log(test);
            ProgressBar.fillAmount = test;
        }
    }
    public void Submit()
    {
        InputField.text = "";
        _minigameManagerScript.DestroyTicket();
    }
}
