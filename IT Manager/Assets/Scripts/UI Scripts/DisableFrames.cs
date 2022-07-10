using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFrames : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Frames = new GameObject[3];
    
    private void Start()
    {
        foreach (GameObject frame in Frames)
        {
            frame.SetActive(!frame.activeSelf);
        }
    }
}
