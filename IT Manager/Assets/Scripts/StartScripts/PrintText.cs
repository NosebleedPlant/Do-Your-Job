using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
public class PrintText : MonoBehaviour
{
    [SerializeField]private float StartDelay;
    [SerializeField] private TextMeshProUGUI[] textObjects;
    private string[] textFull;
    private StringBuilder textVisable = new StringBuilder();
    private void Start()
    {
        textFull=new string[textObjects.Length];
        for(int i=0;i<textObjects.Length;i++)
        {
            textFull[i] = textObjects[i].text;
            textObjects[i].text = "";
        }
        StartCoroutine(printText());
    }

    // Update is called once per frame
    private IEnumerator printText()
    {
        yield return new WaitForSeconds(StartDelay);
        for(int i=0;i<textObjects.Length;i++)
        {
            textVisable.Clear();
            foreach(char c in textFull[i])
            {
                textVisable.Append(c);
                textObjects[i].text = textVisable.ToString();
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}
