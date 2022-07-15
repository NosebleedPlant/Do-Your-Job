using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage_SubManager : MonoBehaviour
{
    //file parameters
    [SerializeField] private GameObject[] FilePrefabs;
    [SerializeField] private Transform prefabContainer;
    [SerializeField] public Collider2D InitialTargetArea;
    [SerializeField] private Collider2D SecondaryTargetArea;
    private Collider2D[] _spawnAreas;
    private Transform _frameTransform;
    private Collider2D _playerObject;
    public float shakeAmount;
    [SerializeField] Transform debug;

    //GameState
    [SerializeField] public GameStatusData gameData;

    private void Awake()
    {
        _spawnAreas = prefabContainer.GetComponents<Collider2D>();
        GameObject frame_temp = GameObject.Find("StorageFrame");
        _frameTransform = frame_temp.GetComponent<Transform>();
        _playerObject = GameObject.Find("STMG_PlayerObject").GetComponent<Collider2D>();
        movePlayerObject = _MovePlayerObject;
    }

    private void OnEnable() => StartCoroutine(SpawnRoutine());

    private void OnDisable() => StopAllCoroutines();

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while(enabled)
        {
            if(gameData.StorageGameData.FileCount<gameData.StorageGameData.MaxFileCount)
            {
                Spawn();
                gameData.StorageGameData.FileCount++;
            }
            
            yield return new WaitForSeconds(gameData.StorageGameData.CurrentSpawnDelay);
        }
    }

    public void Spawn(Transform parentFolder=null)
    {
        int prefabIndex = UnityEngine.Random.Range((parentFolder==null)?0:2, FilePrefabs.Length);
        GameObject prefab = FilePrefabs[prefabIndex];
        Vector2 spawnPosition = (parentFolder==null)?CalculatePrefabPoisiton():parentFolder.position;
        GameObject spawnedfile = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedfile.transform.SetParent(prefabContainer);
    }

    public Vector2 CalculatePrefabPoisiton()
    {
        Vector2 position;
        Collider2D spawnArea = _spawnAreas[UnityEngine.Random.Range(0, _spawnAreas.Length)];
        position.x = UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        position.y = UnityEngine.Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        return position;
    }

    public Vector2 CalculatePrefabVelocity(Vector2 position,bool child)
    {
        Collider2D targetArea = (child)?InitialTargetArea:SecondaryTargetArea;
        float speed = UnityEngine.Random.Range(gameData.StorageGameData.CurrentSpeedRange.x, gameData.StorageGameData.CurrentSpeedRange.y);            
        Vector3 initialTargetDirection = new Vector3();
        initialTargetDirection.x = UnityEngine.Random.Range(targetArea.bounds.min.x, SecondaryTargetArea.bounds.max.x);
        initialTargetDirection.y = UnityEngine.Random.Range(targetArea.bounds.min.y, SecondaryTargetArea.bounds.max.y);
        initialTargetDirection.z = 0;
        
        Vector2 direction = initialTargetDirection - new Vector3(position.x,position.y,0f);
        return direction.normalized * speed;
    }

    public void HandleFileDeath() 
    {
        gameData.StorageGameData.FileCount--;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float timer = 0;
        float max = 0.1f;
        Vector3 originalPosition = _frameTransform.position;
        while (timer<max)
        {
            timer += Time.deltaTime;
            Vector3 offset = UnityEngine.Random.insideUnitCircle*(Time.deltaTime*shakeAmount);
            _frameTransform.position+=offset;
            yield return null;
        }
        _frameTransform.position = originalPosition;
    }

    public void IncrementFileCount() => gameData.StorageGameData.FileCount++;

    public Action<Vector3>movePlayerObject;
    private void _MovePlayerObject(Vector3 position)
    {
        if(_frameTransform.GetSiblingIndex()!=4){return;}
        position += transform.position -_frameTransform.transform.position;
        _playerObject.transform.position = position;
        StartCoroutine(enableDeleteTool());
        
    }

    private IEnumerator enableDeleteTool()
    {
        _playerObject.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _playerObject.enabled = false;
    }
}
