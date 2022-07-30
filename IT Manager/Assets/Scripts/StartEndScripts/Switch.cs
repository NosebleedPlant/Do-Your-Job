using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    [SerializeField]private float StartDelay;
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(StartDelay);
        transform.gameObject.SetActive(false);
    }
}
