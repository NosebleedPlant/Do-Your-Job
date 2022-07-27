using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security_TriggerButtons : MonoBehaviour
{
    [SerializeField] private GameObject Sprite;
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite WrongSprite;
    [SerializeField] private Color ActiveColor;
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
        _renderer = Sprite.GetComponent<SpriteRenderer>();
        _startColor = _renderer.color;
        _startTex = _renderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _active = true;
        note = other.transform;
        LeanTween.color(Sprite,WrongColor,0.1f);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _active = false;
        note = null;
        LeanTween.color(Sprite,_startColor,0.1f);
    }

    public void Activate()
    {
        if(_active && note!=null)
        {
            LeanTween.scale(Sprite,new Vector3(0.6f,0.6f,0.6f),0.1f).setOnStart
            (
                ()=>
                {
                    _renderer.sprite = ActiveSprite;
                    LeanTween.color(Sprite,ActiveColor,0.1f);
                }
            ).setOnComplete
            (
                ()=>
                {
                    _renderer.sprite = _startTex;
                    LeanTween.scale(Sprite,new Vector3(0.5f,0.5f,0.5f),0.1f);
                    LeanTween.color(Sprite,_startColor,0.1f);
                }
            );
            Bounce();
            Destroy(note.gameObject);
        }
        else
        {
            _renderer.sprite = WrongSprite;
            LeanTween.color(Sprite,WrongColor,0.1f).setOnComplete
            (
                ()=>
                {
                    _renderer.sprite = _startTex;
                    LeanTween.color(Sprite,_startColor,0.1f);
                }
            );
        }
    }

    private void Bounce()
    {
        LeanTween.cancel(transform.gameObject);
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
