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
    [SerializeField] private Transform FrameTransform;
    private SpriteRenderer _renderer;
    private Sprite _startTex;
    private Color _startColor;
    private LayerMask _virusMask;
    private bool _active = false;
    private Transform note;

    private void Awake()
    {
        _virusMask= LayerMask.GetMask("SCMG_Virus");
        _renderer = SpriteObject.GetComponent<SpriteRenderer>();
        _startColor = _renderer.color;
        _startTex = _renderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _active = true;
        note = other.transform;
        LeanTween.color(SpriteObject,ActiveColor,0.1f);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _active = false;
        note = null;
        LeanTween.color(SpriteObject,_startColor,0.1f);
    }

    public void Activate()
    {
        if(_active && note!=null)
        {
            _renderer.sprite = ActiveSprite;
            LeanTween.scale(SpriteObject,new Vector3(0.6f,0.6f,0.6f),0.15f).setOnStart
            (
                ()=>
                {LeanTween.color(SpriteObject,CorrectColor,0.1f);}
            ).setOnComplete
            (
                ()=>
                {
                    _renderer.sprite = _startTex;
                    LeanTween.scale(SpriteObject,new Vector3(0.5f,0.5f,0.5f),0.1f);
                    LeanTween.color(SpriteObject,_startColor,0.1f);
                }
            );
            Bounce();
            Destroy(note.gameObject);
        }
        else
        {
            _renderer.sprite = WrongSprite;
            LeanTween.color(SpriteObject,WrongColor,0.1f).setOnComplete
            (
                ()=>
                {
                    _renderer.sprite = _startTex;
                    LeanTween.color(SpriteObject,_startColor,0.1f);
                }
            );
        }
    }

    private void Bounce()
    {
        LeanTween.cancel(FrameTransform.gameObject);
        FrameTransform.localScale= new Vector3(0.850176f,0.850176f,0.999936f);
        LeanTween.scale(FrameTransform.gameObject,new Vector3(1,1,0.999936f),0.1f).setEasePunch().setOnComplete
        (
            ()=>
            {
                LeanTween.scale(FrameTransform.gameObject,new Vector3(0.850176f,0.850176f,0.999936f),0.1f);
            }
        );
    }
}
