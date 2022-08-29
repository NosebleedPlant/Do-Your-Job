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
    [SerializeField] private Slider ProgressBar;
    [SerializeField] private Transform MiniGameArea;
    [SerializeField] private GameObject FailLine;
    [SerializeField] private Color StartColor,WrongColor;
    [SerializeField] private GlitchEffect GlitchEffectTrigger;
    private AudioSource[] _sfx;
    private Transform _frameTransform;
    private LayerMask _triggerMask;
    private float _elapsedTime = 0;

    //GameState
    [SerializeField] public GameStatusData gameData;  
    [SerializeField] private GameManager manager;
    [SerializeField] private Transform FrameTransform;

    private void Awake()
    {
        activateTrigger = _ActivateTrigger;
        _frameTransform = GameObject.Find("SecurityFrame").transform;
        _triggerMask= LayerMask.GetMask("SCMG_TriggerArea");
        _sfx = GetComponents<AudioSource>();
    }

    private void OnEnable() => StartCoroutine(SpawnRoutine());

    private void OnDisable() => StopAllCoroutines();

    
    private void Update()
    {
        if(_frameTransform.GetSiblingIndex()==MiniGameArea.childCount-1)
        {
            float completion = _elapsedTime/gameData.SecurityGameData.RestoreTime;
            ProgressBar.value = completion;
            _elapsedTime += Time.deltaTime;
            if(completion>=1)
            {
                gameData.SecurityGameData.CurrentDamage--;
                _elapsedTime=0;
                _sfx[0].Play(); 
                manager.ResetSecurityFall();
            }
        }
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

    public void OnFail()
    {
        if(_frameTransform.GetSiblingIndex()!=MiniGameArea.childCount-1){return;}
        _elapsedTime = 0;
        //HAFEEZ AUDIO HERE
        _sfx[1].Play();       
        // LeanTween.color(FailLine,WrongColor,0.2f).setOnComplete
        // (
        //     ()=>
        //     {
        //         LeanTween.color(FailLine,StartColor,0.2f);
        //     }
        // );
    }

    public void Bounce(float size)
    {
        LeanTween.cancel(FrameTransform.gameObject);
        FrameTransform.localScale= new Vector3(0.850176f,0.850176f,0.999936f);
        LeanTween.scale(FrameTransform.gameObject,new Vector3(size,size,0.999936f),0.1f).setEaseInElastic().setOnComplete
        (
            ()=>
            {
                LeanTween.scale(FrameTransform.gameObject,new Vector3(0.850176f,0.850176f,0.999936f),0.1f);
            }
        );
    }
}
