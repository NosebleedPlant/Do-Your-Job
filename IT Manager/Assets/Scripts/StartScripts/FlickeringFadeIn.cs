using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringFadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] Content;
    void Start()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        for (int i=0;i<20;i++)
        {
            float time = Mathf.Lerp(0.06f,0,(float)i/20);
            foreach (GameObject element in Content)
            {
                element.SetActive(false);
            }
            yield return new WaitForSeconds(time);
            foreach (GameObject element in Content)
            {
                element.SetActive(true);
            }
            yield return new WaitForSeconds(time);
        }
    }
}
