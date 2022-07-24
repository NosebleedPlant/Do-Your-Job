using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_SubManager : MonoBehaviour
{
    [SerializeField] private GameObject[] VirusPrefabs;
    [SerializeField] private Transform prefabContainer;
    private Transform _frameTransform;
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
        _frameTransform = GameObject.Find("SecurityFrame").transform;
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
        if(_frameTransform.GetSiblingIndex()!=4){return;}
        position+=transform.position -_frameTransform.position;
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

    public void IncrementVirusCount()  => gameData.SecurityGameData.VirusCount--;

    public void DecrementVirusCount() => gameData.SecurityGameData.VirusCount++;
}
