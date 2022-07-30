using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAudioAfterLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
