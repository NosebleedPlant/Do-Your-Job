using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private GameStatusData _gameData;
    [SerializeField] private TextMeshProUGUI RevenuCounter;
    [SerializeField] private Slider StorageBar;
    [SerializeField] private TextMeshProUGUI StorageETA;
    [SerializeField] private Slider NetworkBar;
    [SerializeField] private TextMeshProUGUI NetworkETA;
    [SerializeField] private TextMeshProUGUI ComplaintCounter;
    [SerializeField] private Image[] SecurityLives;
    [SerializeField] private TextMeshProUGUI SecurityCounter;
    [SerializeField] private GameStatusData _gameData;

    private void Awake()
    {
        //initalize command
        StartCoroutine(_gameData.UpdateRevenue());
        StartCoroutine(_gameData.NetworkGameData.UpdateNetworkUse());
        StartCoroutine(_gameData.SecurityGameData.UpdateSecurity());
    }
    private void Update()
    {
        RevenuCounter.text = "[$<color=#FF3369><b>"+_gameData.CurrentRevenue.ToString("D8")+"</b></color>]";
        
        StorageBar.value = _gameData.StorageGameData.CurrentFill;
        StorageETA.text = "[ETA:"+_gameData.StorageGameData.CurrentFill*100+"%]";
        
        NetworkBar.value = _gameData.NetworkGameData.CurrentFill;
        NetworkETA.text = "[ETA:"+_gameData.NetworkGameData.CurrentFill*100+"%]";
        
        ComplaintCounter.text = "[<color=#FF3369><b>"+_gameData.ComplaintGameData.ComplaintCount+"</b></color> current open/"+_gameData.ComplaintGameData.MaxComplaintCount+" max]";

        UpdateLives();
        SecurityCounter.text = "["+_gameData.SecurityGameData.CurrentLives+"/7]";
    }

    private void OnDisable()
    {
        _gameData.ResetData();
        StopAllCoroutines();
    }

    private void UpdateLives()
    {
        for(int i = 0;i<SecurityLives.Length;i++)
        {
            if(i>=_gameData.SecurityGameData.CurrentLives)
            {
                SecurityLives[i].enabled=false;
            }
            else
            {
                SecurityLives[i].enabled=true;
            }
        }
    }
}
