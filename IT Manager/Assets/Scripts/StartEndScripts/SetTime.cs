using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SetTime : MonoBehaviour
{
    [SerializeField] GameStatusData gameStatusData;
    [SerializeField] TextMeshProUGUI timeText;
    void Update()
    {
        timeText.text = "Lasted: "+gameStatusData.SurvivalTime+ " secs";
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadSceneAsync("Desktop");
        }
    }
}
