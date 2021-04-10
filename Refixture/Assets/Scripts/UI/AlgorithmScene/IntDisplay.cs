using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class IntDisplay : MonoBehaviour
{
    private TextMeshProUGUI _displayTMP;
    [SerializeField] private IntEventChannelSO _intCh;

    void Awake()
    {
        _displayTMP = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _intCh.OnEventRaised += RecievedInt;
    }

    private void OnDisable()
    {
        _intCh.OnEventRaised -= RecievedInt;
    }

    private void RecievedInt(int value)
    {
        _displayTMP.text = value.ToString();
    }
}