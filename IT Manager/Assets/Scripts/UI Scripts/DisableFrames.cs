using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableFrames : MonoBehaviour
{
    [SerializeField]
    private Image[] Frames = new Image[3];
    
    private void Start()
    {
        foreach (Image frame in Frames)
        {
            frame.enabled = false;
        }
    }
}
