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
    public float minSpawnDelay = 0.5f;
    public float maxSpawnDelay = 1f;
    public float minVelocity = 1f;
    public float maxVelocity = 2.5f;

    //GameState
    [SerializeField] public GameStatusData gameData;
    private int _fileCount = 0;
    private int _maxFileCount;
    public int _fileInfolder = 3;

    private void Awake()
    {
        _spawnAreas = prefabContainer.GetComponents<Collider2D>();
        _maxFileCount = 30;
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while(enabled)
        {
            if(_fileCount<_maxFileCount)
            {
                Spawn();
                _fileCount++;
            }
            
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    public void Spawn(Transform parentFolder=null)
    {
        int prefabIndex = Random.Range((parentFolder==null)?0:2, FilePrefabs.Length);
        GameObject prefab = FilePrefabs[prefabIndex];
        Vector2 spawnPosition = (parentFolder==null)?CalculatePrefabPoisiton():parentFolder.position;
        GameObject spawnedfile = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedfile.transform.SetParent(prefabContainer);
    }

    public Vector2 CalculatePrefabPoisiton()
    {
        Vector2 position;
        Collider2D spawnArea = _spawnAreas[Random.Range(0, _spawnAreas.Length)];
        position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        return position;
    }

    public Vector2 CalculatePrefabVelocity(Vector2 position,bool child)
    {
        Collider2D targetArea = (child)?InitialTargetArea:SecondaryTargetArea;
        float speed = Random.Range(minVelocity, maxVelocity);            
        Vector3 initialTargetDirection = new Vector3();
        initialTargetDirection.x = Random.Range(targetArea.bounds.min.x, SecondaryTargetArea.bounds.max.x);
        initialTargetDirection.y = Random.Range(targetArea.bounds.min.y, SecondaryTargetArea.bounds.max.y);
        initialTargetDirection.z = 0;
        
        Vector2 direction = initialTargetDirection - new Vector3(position.x,position.y,0f);
        return direction.normalized * speed;
    }

    public void HandleFileDeath()
    {
        _fileCount--;
    }

    public void IncrementFileCount()
    {_fileCount++;}
}
