using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


[CreateAssetMenu(fileName = "GameStatusData", menuName = "")]
public class GameStatusData : ScriptableObject
{
    [SerializeField] public StorageMiniGameData StorageGameData;
    [SerializeField] public ComplaintMiniGameData ComplaintGameData;
    [SerializeField] public SecurityMiniGameData SecurityGameData;
    [SerializeField] public NetworkMiniGameData NetworkGameData;

    private bool _gameOver = false;
    public bool GameOver{get=>_gameOver;}
    private static int _totalRevenue = 63000000;
    private int _currentRevenu = _totalRevenue;
    public int CurrentRevenue{get=>_currentRevenu;}
    
    public void ResetData()
    {
        StorageGameData = new StorageMiniGameData();
        ComplaintGameData = new ComplaintMiniGameData();
        SecurityGameData = new SecurityMiniGameData();
        NetworkGameData = new NetworkMiniGameData();
        _currentRevenu = 63000000;
    }

    public IEnumerator UpdateRevenue()
    {
        while(_currentRevenu>0)
        {
            yield return new WaitForSeconds(0.1f);

            if(StorageGameData.MaxReached) _currentRevenu-=100000;//change to more natural progression
            if(NetworkGameData.MaxReached) _currentRevenu-=100000;//change to more natural progression

            _currentRevenu = Mathf.Clamp(_currentRevenu,0,_totalRevenue);
        }
        _gameOver = true;
    }
}

[Serializable]
public class StorageMiniGameData
{
    private float _currentDelay = 0.5f;
    public float CurrentSpawnDelay{get=>_currentDelay;}
    private float _minSpawnDelay = 0.5f;
    private float _maxSpawnDelay = 1f;

    private Vector2 _currentSpeedRange = new Vector2(2f,2.5f);
    public Vector2 CurrentSpeedRange{get=>_currentSpeedRange;}
    private Vector2 MaxSpeedRange = new Vector2(3f,4f);

    private int _maxFileCount = 40;
    public int MaxFileCount{get=>_maxFileCount;}
    private bool _maxReached = false;
    public bool MaxReached{get=>_maxReached;}
    private float _currentFill = 0;
    public float CurrentFill{get=>_currentFill;}
    
    private int _fileCount = 0;
    public int FileCount
    {
        get => _fileCount;
        set
        {
            _fileCount=value;
            _fileCount = Mathf.Clamp(_fileCount,0,_maxFileCount);
            _currentFill = (float)_fileCount/MaxFileCount;
            _maxReached = (_fileCount>=MaxFileCount)?true:false;
            UpdateSpeedRange();
            UpdateSpawnDelay();
        }
    }

    public void UpdateSpeedRange()
    {
        _currentSpeedRange.x = Mathf.Lerp(_currentSpeedRange.x,MaxSpeedRange.x,CurrentFill);
        _currentSpeedRange.y = Mathf.Lerp(_currentSpeedRange.y,MaxSpeedRange.y,CurrentFill);
    }

    public void UpdateSpawnDelay()
    {
        _currentSpeedRange.x = Mathf.Lerp(_minSpawnDelay,_maxSpawnDelay,CurrentFill);
    }
}

[Serializable]
public class ComplaintMiniGameData
{
    private float _spawnRate = 6f;
    public float SpawnRate{get=>_spawnRate;}

    private int _maxComplaintCount = 10;
    public float MaxComplaintCount{get=>_maxComplaintCount;}
    private bool _maxReached = false;
    public bool MaxReached{get=>_maxReached;}

    private int _complaintCount = 0;
    public int ComplaintCount
    {
        get => _complaintCount;
        set
        {
            _complaintCount=value;
            _complaintCount = Mathf.Clamp(_complaintCount,0,_maxComplaintCount);
            _maxReached = (_complaintCount>=MaxComplaintCount)?true:false;
        }
    }
}

[Serializable]
public class NetworkMiniGameData
{
    private int _max = 100;
    private int _current = 0;
    public int Current
    {
        get=>_current;
        set
        {
            _current = value;
            _current = Mathf.Clamp(_current,0,_max);
        }
    }
    private float _fillRate = 1f;
    public float FillRate{get=>_fillRate;}
    public float CurrentFill{get=>(float)_current/_max;}
    private bool _maxReached = false;
    public bool MaxReached{get=>_maxReached;}

    public IEnumerator UpdateNetworkUse()
    {
        while(true)
        {
            Current+=UnityEngine.Random.Range(1,3);
            _maxReached = (_current>=_max)? true:false;
            yield return new WaitForSeconds(_fillRate);
        }
    }

}

[Serializable]
public class SecurityMiniGameData
{
    private float _fallRate = 6f;
    private int _maxDamage = 7;
    public int MaxDamage {get=>_maxDamage;}
    private int _currentDamage = 0;
    public int CurrentDamage 
    {
        get=>_currentDamage; 
        set
        {
            _currentDamage = Mathf.Clamp(value,0,_maxDamage);
            _maxReached = (_currentDamage>=_maxDamage)? true:false;
            Debug.Log(_maxReached);
        }
    }

    private bool _maxReached = false;
    private bool MinReached {get=>_maxReached;}

    public IEnumerator UpdateSecurity()
    {
        while(true)
        {
            yield return new WaitForSeconds(_fallRate);
            CurrentDamage++;
        }
    }
}