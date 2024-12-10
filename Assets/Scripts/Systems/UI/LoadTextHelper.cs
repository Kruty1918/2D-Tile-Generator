using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class LoadTextHelper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private List<TextInfos> textInfos;

    private void Start()
    {
        text.text = textInfos[0].textInfo;

        StartCoroutine(ChangeText());
    }

    private IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(Random.Range(4, 6));

        string updInfo = "";

        while (true)
        {
            TextInfos info = textInfos[Random.Range(0, textInfos.Count)];

            if (info.weight > Random.value)
            {
                updInfo = info.textInfo;
                text.text = updInfo; 
                break;
            }
        }

        yield return StartCoroutine(ChangeText()); 
    }
}

[System.Serializable]
public struct TextInfos
{
    public string textInfo;

    [Range(0f, 1f)]
    public float weight;
}