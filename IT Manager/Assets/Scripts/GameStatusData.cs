using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


[CreateAssetMenu(fileName = "GameStatusData", menuName = "")]
public class GameStatusData : ScriptableObject
{    
    private double time;
    [SerializeField]public StorageMiniGameData StorageGameData;
    [SerializeField]public ComplaintMiniGameData ComplaintGameData;
    [SerializeField]public SecurityMiniGameData SecurityGameData;

    public void ResetData()
    {
        Debug.Log("reset");
        time = 0;
        StorageGameData = new StorageMiniGameData();
        ComplaintGameData = new ComplaintMiniGameData();
        SecurityGameData = new SecurityMiniGameData();
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
    private float _spawnRate = 2f;
    public float SpawnRate{get=>_spawnRate;}

    private int _maxComplaintCount = 10;
    public float MaxComplaintCount{get=>_maxComplaintCount;}
    private bool _maxReached = false;
    public bool MaxReached{get=>_maxReached;}

    private int _visableMaxCount = 4;
    public float VisableMaxCount{get=>_visableMaxCount;}
    private bool _maxVisableReached = false;
    public bool MaxVisableReached{get=>_maxVisableReached;}

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

    private int _currentVisableCount = 0;
    public int CurrentVisableCount
    {
        get => _currentVisableCount;
        set
        {
            _currentVisableCount=value;
            _currentVisableCount = Mathf.Clamp(_currentVisableCount,0,_visableMaxCount);
            _maxVisableReached = (_currentVisableCount>=VisableMaxCount)?true:false;
        }
    }
}

[Serializable]
public class SecurityMiniGameData
{
    private float _minSpawnDelay = 0.5f;
    public float MinSpawnDelay{get=>_minSpawnDelay;}
    private float _maxSpawnDelay = 1f;
    public float MaxSpawnDelay{get=>_maxSpawnDelay;}

    private Vector2 _currentSpeedRange = new Vector2(2f,2.5f);
    public Vector2 CurrentSpeedRange{get=>_currentSpeedRange;}
    private Vector2 MaxSpeedRange = new Vector2(3f,3.5f);

    private int _maxVirusCount = 40;
    public int MaxVirusCount{get=>_maxVirusCount;}
    private bool _maxReached = false;
    public bool MaxReached{get=>_maxReached;}
    private float _currentFill = 0;
    public float CurrentFill{get=>_currentFill;}
    
    private int _virusCount = 0;
    public int VirusCount
    {
        get => _virusCount;
        set
        {
            _virusCount=value;
            _virusCount = Mathf.Clamp(_virusCount,0,_maxVirusCount);
            _currentFill = (float)_virusCount/MaxVirusCount;
            _maxReached = (_virusCount>=MaxVirusCount)?true:false;
        }
    }
}