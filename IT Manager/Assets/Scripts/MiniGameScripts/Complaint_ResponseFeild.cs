using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Complaint_ResponseFeild : MonoBehaviour
{
    [SerializeField] private TMP_InputField InputField;
    [SerializeField] private Image ProgressBar;
    [SerializeField] private GameObject SubmitButton;
    [SerializeField] private GameObject MinigameManager;
    private Complaint_SubManager _minigameManagerScript;
    private string[] _messages = {"its done.","fixed it!","trying my best here ;-;","did u try googling it?","did you try turning it on and off?","Im sorry of the inconvience its fixed now.","u need help? buddy IM the one that needs help","beats me lol.","please dont fire me.."};
    private string _message;
    // private static int maxLength = _message.Length;
    public bool completed = false;

    private void Awake()
    {
        _message = _messages[Random.Range(0,_messages.Length-1)];
        _minigameManagerScript = MinigameManager.GetComponent<Complaint_SubManager>();
        InputField.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(string text)
    {
        int maxLength = _message.Length;
        int length = text.Length;
        completed = (maxLength==length)?true:false;
        if(length>0 && length<=maxLength)
        {
            //cancel any tweens
            LeanTween.cancel(SubmitButton);
            SubmitButton.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            //replace text
            string sub = _message.Substring(0, length);
            InputField.text = sub;
            //update progress
            ProgressBar.fillAmount = (float)length/maxLength;
        }
        else if(length>maxLength)
        {
            //cancel any tweens
            LeanTween.cancel(SubmitButton);
            SubmitButton.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            ProgressBar.fillAmount = 2-(float)length/maxLength;
        }
        if(length==maxLength)
        {
            //pulse when ready
            LeanTween.scale(SubmitButton,new Vector3(0.6f,0.6f,0.6f),0.2f).setLoopPingPong();
        }
    }
    public void Submit()
    {
        //reset field and bar
        InputField.text = "";
        ProgressBar.fillAmount = 0;
        //cancel any tweens
        LeanTween.cancel(SubmitButton);
        SubmitButton.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        //select new message
        _message = _messages[Random.Range(0,_messages.Length-1)];
        //delete the complaint
        _minigameManagerScript.DeleteComplaint();
    }
}
