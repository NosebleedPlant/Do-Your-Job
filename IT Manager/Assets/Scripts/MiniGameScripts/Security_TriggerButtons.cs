using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_TriggerButtons : MonoBehaviour
{
    [SerializeField] private GameObject SpriteObject;
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite WrongSprite;
    [SerializeField] private Color ActiveColor;
    [SerializeField] private Color CorrectColor;
    [SerializeField] private Color WrongColor;
    
    private AudioSource[] _buttonSfx;
    private SpriteRenderer _renderer;
    private Sprite _startTex;
    private Color _startColor;
    private LayerMask _virusMask;
    private bool _active = false;
    private bool _triggered  = false;
    private Transform note;

    private void Awake()
    {
        _virusMask= LayerMask.GetMask("SCMG_Virus");
        _renderer = SpriteObject.GetComponent<SpriteRenderer>();
        _startColor = _renderer.color;
        _startTex = _renderer.sprite;
        _buttonSfx = GetComponentsInParent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("SCMG_Virus"))
        {
            _active = true;
            note = other.transform;
            if(!_triggered) LeanTween.color(SpriteObject,ActiveColor,0.1f);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.CompareTag("SCMG_Virus"))
        {
            _active = false;
            note = null;
            LeanTween.color(SpriteObject,_startColor,0.1f);
        }
    }

    public void Activate()
    {
        _triggered=true;
        if(_active && note!=null)
        {
            _buttonSfx[0].Play();
            _renderer.sprite = ActiveSprite;
            LeanTween.scale(SpriteObject,new Vector3(0.6f,0.6f,0.6f),0.15f).setOnStart
            (
                ()=>
                {LeanTween.color(SpriteObject,CorrectColor,0.1f);}
            ).setOnComplete
            (
                ()=>
                {
                    _triggered=false;
                    _renderer.sprite = _startTex;
                    LeanTween.scale(SpriteObject,new Vector3(0.5f,0.5f,0.5f),0.1f);
                    LeanTween.color(SpriteObject,_startColor,0.1f);
                }
            );
            Destroy(note.gameObject);
        }
        else
        {
            _buttonSfx[1].Play();
            _renderer.sprite = WrongSprite;
            LeanTween.color(SpriteObject,WrongColor,0.1f).setOnComplete
            (
                ()=>
                {
                    _triggered=false;
                    _renderer.sprite = _startTex;
                    LeanTween.color(SpriteObject,_startColor,0.1f);
                }
            );
        }
    }
}
