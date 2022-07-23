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
    [SerializeField] private GameStatusData _gameData;

    private void Awake()
    {
        //initalize command
        StartCoroutine(_gameData.UpdateRevenue());
        StartCoroutine(_gameData.NetworkGameData.UpdateNetworkUse());
    }
    private void Update()
    {
        RevenuCounter.text = "[$<color=#FF3369><b>"+_gameData.CurrentRevenue.ToString("D8")+"</b></color>]";
        
        StorageBar.value = _gameData.StorageGameData.CurrentFill;
        StorageETA.text = "[ETA:"+_gameData.StorageGameData.CurrentFill*100+"%]";
        
        NetworkBar.value = _gameData.NetworkGameData.CurrentFill;
        NetworkETA.text = "[ETA:"+_gameData.NetworkGameData.CurrentFill*100+"%]";
        
        ComplaintCounter.text = "[<color=#FF3369><b>"+_gameData.ComplaintGameData.ComplaintCount+"</b></color> current open/"+_gameData.ComplaintGameData.MaxComplaintCount+" max]";
    }

    private void OnDisable()
    {
        _gameData.ResetData();
        StopAllCoroutines();
    }
}
