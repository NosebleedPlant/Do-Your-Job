using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float delay;
    [SerializeField] private float loadTime;
    
    AsyncOperation loadingOperation;

    void Start()
    {
        loadingOperation = SceneManager.LoadSceneAsync("Desktop");
        this.loadingOperation.allowSceneActivation = false;
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        yield return new WaitForSeconds(delay);
        LeanTween.value(transform.gameObject,fill,0,1,loadTime).setOnComplete
        (
            ()=>
            {
                this.loadingOperation.allowSceneActivation = true;
            }
        );
    }

    void fill(float value)
    {
        slider.value = value;
    }
}
