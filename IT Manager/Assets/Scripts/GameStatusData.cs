using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameStatusData", menuName = "")]
public class GameStatusData : ScriptableObject
{
    public StorageMiniGameData StorageGameData;
    public ComplaintMiniGameData ComplaintGameData;
}

[Serializable]
public class StorageMiniGameData
{
    public float CurrentFill = 0;
    public int FileCount = 0;
    public int MaxFileCount = 30;
    public bool MaxReached = false;

    private void IncreaseCount()
    {
        MaxReached = (FileCount>=MaxFileCount)?true:false;
        if(!MaxReached)
        {
            FileCount++;
            UpdateFill();
        }
    }

    public void DecreaseFill()
    {
        MaxReached = (FileCount>=MaxFileCount)?true:false;
        if(!MaxReached)
        {
            FileCount++;
            UpdateFill();
        }
    }

    private void UpdateFill()
    {
        CurrentFill = (float)FileCount/MaxFileCount;
        Mathf.Clamp(CurrentFill,0,1);
    }
}

[Serializable]
public class ComplaintMiniGameData
{
    public int ComplaintCount = 0;
    public readonly int MaxComplaints = 100;
    public float ComplaintRate = 0;
    public bool MaxReached = false;
}