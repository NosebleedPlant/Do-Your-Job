using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_SubManager : MonoBehaviour
{
    private void start()
    {
        InputManagerV2.current.onMiniGameClickTirggerd +=test;
    }

    private void test()
    {
        Debug.Log("hello");
    }
}
