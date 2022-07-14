using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_SubManager : MonoBehaviour
{
    [SerializeField] private GameObject[] VirusPrefabs;
    [SerializeField] private Transform prefabContainer;
    private Vector3 _framePosition;
    private Rigidbody2D _playerObject;
    private Collider2D _spawnArea;
    private Collider2D _targetArea;

    //GameState
    [SerializeField] public GameStatusData gameData;

    //MOVE TO DATACONTAINER 
    

    private void Awake()
    {
        movePlayerObject = _MovePlayerObject;
        _playerObject = transform.Find("SCMG_PlayerObject").GetComponent<Rigidbody2D>();
        _framePosition = GameObject.Find("SecurityFrame").transform.position;
        _spawnArea = transform.Find("SCMG_Spawner").GetComponent<Collider2D>();
        _targetArea = transform.Find("SCMG_TargetArea").GetComponent<Collider2D>();
    }

    private void OnEnable() => StartCoroutine(SpawnRoutine());

    private void OnDisable() => StopAllCoroutines();

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);
        while(enabled)
        {
            if(gameData.SecurityGameData.VirusCount<gameData.SecurityGameData.MaxVirusCount)
            {
                Spawn();
                gameData.SecurityGameData.VirusCount++;
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(gameData.SecurityGameData.MinSpawnDelay, gameData.SecurityGameData.MaxSpawnDelay));
        }
    }

    public void Spawn(Transform parentFolder=null)
    {
        int prefabIndex = UnityEngine.Random.Range((parentFolder==null)?0:2, VirusPrefabs.Length);
        GameObject prefab = VirusPrefabs[prefabIndex];
        Vector2 spawnPosition = (parentFolder==null)?CalculatePrefabPoisiton():parentFolder.position;
        GameObject spawnedfile = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedfile.transform.SetParent(prefabContainer);
    }

    public Action<Vector3> movePlayerObject;
    private void _MovePlayerObject(Vector3 position)
    {
        position+=transform.position -_framePosition;
        position = new Vector3(position.x,0f);
        _playerObject.MovePosition(position);
    }

    public Vector2 CalculatePrefabPoisiton()
    {
        Vector2 position;
        position.x = UnityEngine.Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x);
        position.y = UnityEngine.Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y);
        return position;
    }

    public Vector2 CalculatePrefabVelocity(Vector2 position)
    {
        float speed = UnityEngine.Random.Range(gameData.SecurityGameData.CurrentSpeedRange.x, gameData.SecurityGameData.CurrentSpeedRange.y);            
        Vector3 initialTargetDirection = new Vector3();
        initialTargetDirection.x = UnityEngine.Random.Range(_targetArea.bounds.min.x, _targetArea.bounds.max.x);
        initialTargetDirection.y = UnityEngine.Random.Range(_targetArea.bounds.min.y, _targetArea.bounds.max.y);
        initialTargetDirection.z = 0;
        
        Vector2 direction = initialTargetDirection - new Vector3(position.x,position.y,0f);
        return direction.normalized * speed;
    }

    public void IncrementVirusCount()  => gameData.SecurityGameData.VirusCount--;

    public void DecrementVirusCount() => gameData.SecurityGameData.VirusCount++;
}
