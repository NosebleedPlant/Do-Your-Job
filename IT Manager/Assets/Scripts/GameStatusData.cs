using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "GameStatusData", menuName = "")]
public class GameStatusData : ScriptableObject
{
    private double time;
    public StorageMiniGameData StorageGameData;
    public ComplaintMiniGameData ComplaintGameData;
}

[Serializable]
public class StorageMiniGameData
{
    [SerializeField]private int _maxFileCount = 40;
    public int MaxFileCount{get=>_maxFileCount;}
    [SerializeField]private float _minSpawnDelay = 0.5f;
    public float MinSpawnDelay{get=>_minSpawnDelay;}
    [SerializeField]private float _maxSpawnDelay = 1f;
    public float MaxSpawnDelay{get=>_maxSpawnDelay;}
    [SerializeField]private Vector2 _currentSpeedRange = new Vector2(2f,2.5f);
    public Vector2 CurrentSpeedRange{get=>_currentSpeedRange;}
    [SerializeField]private Vector2 MaxSpeedRange = new Vector2(3f,3.5f);
    private bool _maxReached = false;
    public bool MaxReached{get=>_maxReached;}
    private float _currentFill = 0;
    public float CurrentFill{get=>_currentFill;}
    
    [SerializeField]private int _fileCount = 0;
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
        }
    }

    public void UpdateSpeedRange()
    {
        _currentSpeedRange.x = Mathf.Lerp(_currentSpeedRange.x,MaxSpeedRange.x,CurrentFill);
        _currentSpeedRange.y = Mathf.Lerp(_currentSpeedRange.y,MaxSpeedRange.y,CurrentFill);
    }
}

[Serializable]
public class ComplaintMiniGameData
{
    [SerializeField]private float _spawnRate = 2f;
    public float SpawnRate{get=>_spawnRate;}

    [SerializeField]private int _maxComplaintCount = 10;
    public float MaxComplaintCount{get=>_maxComplaintCount;}
    [SerializeField]private bool _maxReached = false;
    public bool MaxReached{get=>_maxReached;}

    [SerializeField]private int _visableMaxCount = 4;
    public float VisableMaxCount{get=>_visableMaxCount;}
    [SerializeField]private bool _maxVisableReached = false;
    public bool MaxVisableReached{get=>_maxVisableReached;}

    [SerializeField]private int _complaintCount = 0;
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

    [SerializeField]private int _currentVisableCount = 0;
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