using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FloatDisplay : MonoBehaviour
{
    private TextMeshProUGUI _displayTMP;
    [SerializeField] private FloatEventChannelSO _floatCh;

    void Awake()
    {
        _displayTMP = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _floatCh.OnEventRaised += RecievedFloat;
    }

    private void OnDisable()
    {
        _floatCh.OnEventRaised -= RecievedFloat;
    }

    private void RecievedFloat(float value)
    {
        _displayTMP.text = value.ToString("F2");
    }
}
