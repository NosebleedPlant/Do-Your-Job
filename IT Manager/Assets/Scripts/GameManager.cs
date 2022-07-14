using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private GameStatusData _gameData;
    [SerializeField] private Slider _storageBar;
    [SerializeField] private TextMeshProUGUI _complaintCounter;
    [SerializeField] private GameStatusData _gameData;

    private void Awake()
    {
        //initalize command
    }
    private void Update()
    {
        _storageBar.value = _gameData.StorageGameData.CurrentFill;
        _complaintCounter.text = "[<color=#FF3369><b>"+_gameData.ComplaintGameData.ComplaintCount+"</b></color> current open/"+_gameData.ComplaintGameData.MaxComplaintCount+" max]";
    }

    private void OnDisable()
    {
        _gameData.ResetData();
    }
}
