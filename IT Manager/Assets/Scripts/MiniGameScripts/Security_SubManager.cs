using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Security_SubManager : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] VirusPrefabs;
    [SerializeField] private Transform PrefabContainer;
    [SerializeField] private Image ProgressBar;
    [SerializeField] private Transform MiniGameArea;
    private Transform _frameTransform;
    private LayerMask _triggerMask;
    public float _elapsedTime = 0;
    private float _resotreTime = 3f;

    //GameState
    [SerializeField] public GameStatusData gameData;  

    private void Awake()
    {
        activateTrigger = _ActivateTrigger;
        _frameTransform = GameObject.Find("SecurityFrame").transform;
        _triggerMask= LayerMask.GetMask("SCMG_TriggerArea");
    }

    private void OnEnable() => StartCoroutine(SpawnRoutine());

    private void OnDisable() => StopAllCoroutines();

    private void Update()
    {
        float completion = _elapsedTime/_resotreTime;
        ProgressBar.fillAmount = completion;
        _elapsedTime += Time.deltaTime;
        if(completion>=1){gameData.SecurityGameData.CurrentDamage--;_elapsedTime=0;}//HAFEEZ ADD AUDIO HERE
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);
        while(enabled)
        {
            Spawn();
            yield return new WaitForSeconds(gameData.SecurityGameData.SpawnRate);
            //0.17f
        }
    }

    public void Spawn()
    {
        int prefabIndex = UnityEngine.Random.Range(0, VirusPrefabs.Length);
        GameObject prefab = VirusPrefabs[prefabIndex];
        Vector2 spawnPosition = CalculatePrefabPoisiton();
        GameObject spawnedfile = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedfile.transform.SetParent(PrefabContainer);
    }

    public Action<Vector3> activateTrigger;
    private void _ActivateTrigger(Vector3 position)
    {
        if(_frameTransform.GetSiblingIndex()!=MiniGameArea.childCount-1){return;}
        position += transform.position -_frameTransform.position;
        Collider2D overlap = Physics2D.OverlapPoint(position,_triggerMask);
        if( overlap!=null)
        {
            overlap.SendMessage("Activate");
        }
    }

    public Vector2 CalculatePrefabPoisiton()
    {
        Vector2 position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position;
        return position;
    }
}
