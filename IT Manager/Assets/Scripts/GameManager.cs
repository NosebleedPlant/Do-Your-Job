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
    [SerializeField] private TextMeshProUGUI ComplaintCounter;
    [SerializeField] private GameStatusData _gameData;

    private void Awake()
    {
        //initalize command
        StartCoroutine(_gameData.UpdateRevenue());
    }
    private void Update()
    {
        RevenuCounter.text = "[$<color=#FF3369><b>"+_gameData.CurrentRevenue+"</b></color>]";
        StorageBar.value = _gameData.StorageGameData.CurrentFill;
        StorageETA.text = "[ETA:"+_gameData.StorageGameData.CurrentFill*100+"%]";
        ComplaintCounter.text = "[<color=#FF3369><b>"+_gameData.ComplaintGameData.ComplaintCount+"</b></color> current open/"+_gameData.ComplaintGameData.MaxComplaintCount+" max]";
    }

    private void OnDisable()
    {
        _gameData.ResetData();
        StopCoroutine(_gameData.UpdateRevenue());
    }
}
