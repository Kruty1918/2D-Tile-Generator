using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLimiter : MonoBehaviour
{
    [SerializeField] private TMP_InputField field;
    [SerializeField] private int maxSumbol = 15;

    private void Start()
    {
        field.onValueChanged.AddListener(OnValueChanget);
    }

    private void OnValueChanget(string channame)
    {
        if (IsMaxSymbol(channame)) field.text = channame.Substring(0, maxSumbol);
    }

    private bool IsMaxSymbol(string channame)
    {
        return channame.Length > maxSumbol;
    }
}
