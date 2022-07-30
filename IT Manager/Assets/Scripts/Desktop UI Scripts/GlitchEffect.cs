using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    [SerializeField] Material GlitchMaterial;
    public void Trigger()
    {
        Debug.Log("test");
        StopCoroutine(SplitShift());
        GlitchMaterial.SetFloat("_ROffset",0);
        GlitchMaterial.SetFloat("_GOffset",0);
        StartCoroutine(SplitShift());
    }

    private IEnumerator SplitShift()
    {
        float duration = 0.25f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            GlitchMaterial.SetFloat("_ROffset",Random.Range(-0.02f,0.02f));
            GlitchMaterial.SetFloat("_GOffset",Random.Range(-0.02f,0.02f));
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        GlitchMaterial.SetFloat("_ROffset",0);
        GlitchMaterial.SetFloat("_GOffset",0);
    }

    private void OnDisable()
    {
        GlitchMaterial.SetFloat("_ROffset",0);
        GlitchMaterial.SetFloat("_GOffset",0);
    }
}
