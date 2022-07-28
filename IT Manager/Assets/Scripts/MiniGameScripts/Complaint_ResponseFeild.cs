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
    [SerializeField] private Transform FrameTransform;
    [SerializeField] private float ShakeAmount = 2.81f; 
    private AudioSource _keyboardSfx;
    private Complaint_SubManager _minigameManagerScript;
    private string[] _messages = 
    {
        "its done.",
        "fixed it!",
        "trying my best here ;-;",
        "did u try googling it?",
        "did you try turning it on and off?",
        "Im sorry for the inconvience its fixed now.",
        "u need help? buddy IM the one that needs help",
        "beats me lol.",
        "please dont fire me.."
    };
    private string _message;
    private Vector3 submitScale = new Vector3(0.56f,0.56f,0.56f);
    // private static int maxLength = _message.Length;
    public bool completed = false;

    private void Awake()
    {
        _message = _messages[Random.Range(0,_messages.Length-1)];
        _minigameManagerScript = MinigameManager.GetComponent<Complaint_SubManager>();
        InputField.onValueChanged.AddListener(OnValueChange);
        _keyboardSfx = GetComponent<AudioSource>();
    }

    private void OnDisable() =>  StopAllCoroutines();

    private void OnValueChange(string text)
    {
        int maxLength = _message.Length;
        int length = text.Length;
        completed = (maxLength==length)?true:false;
        if(length>0 && length<=maxLength)
        {
            //cancel any tweens
            LeanTween.cancel(SubmitButton);
            SubmitButton.transform.localScale = submitScale;
            //replace text
            string sub = _message.Substring(0, length);
            InputField.text = sub;
            //update progress
            ProgressBar.fillAmount = (float)length/maxLength;
            
            StartCoroutine(Shake());
        }
        else if(length>maxLength)
        {
            //cancel any tweens
            LeanTween.cancel(SubmitButton);
            SubmitButton.transform.localScale = submitScale;
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
        SubmitButton.transform.localScale = submitScale;
        //select new message
        _message = _messages[Random.Range(0,_messages.Length-1)];
        //delete the complaint
        _minigameManagerScript.DeleteComplaint();
    }

    private IEnumerator Shake()
    {
        float timer = 0;
        float max = 0.1f;
        Vector3 originalPosition = FrameTransform.position;
        while (timer<max)
        {
            timer += Time.deltaTime;
            Vector3 offset = UnityEngine.Random.insideUnitCircle*(Time.deltaTime*ShakeAmount);
            FrameTransform.position+=offset;
            yield return null;
        }
        FrameTransform.position = originalPosition;
    }

    private void PlayKbClick()
    {
        _keyboardSfx.volume = Random.Range(0.9f, 1f);
        _keyboardSfx.pitch = Random.Range(0.8f, 1.1f);
        _keyboardSfx.Play();
    }
}
