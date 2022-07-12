using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStatusData _gameData;
    [SerializeField] private Slider _storageBar;
    [SerializeField] private STMG_Spawner _storageGameSpawner;

    private void Awake()
    {
        //initalize command
    }
    private void Update()
    {
        _storageBar.value = _gameData.StorageGameData.CurrentFill;
    }
}
