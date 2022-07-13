using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerV2 : MonoBehaviour
{
    public static InputManagerV2 current;

    private void Awake()
    {
        current = this;
    }

    public event Action onMiniGameClickTirggerd;

    public void MiniGameClick()
    {
        if (onMiniGameClickTirggerd!=null)
        {
            onMiniGameClickTirggerd();
        }
    }
}
