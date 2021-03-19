using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParameterUI : MonoBehaviour
{
    [SerializeField] private GameObject _popDropdown;
    [SerializeField] private GameObject _mutSlider;
    [SerializeField] private GameObject _crossSlider;
    [SerializeField] private SettingsSO _settingsSO;

    private IntEventChannelSO _popChannel;
    private FloatEventChannelSO _mutChannel;
    private FloatEventChannelSO _crossChannel;

    private void Awake()
    {
        _popChannel = _settingsSO.PopChangedChannel;
        _mutChannel = _settingsSO.MutationChangedChannel;
        _crossChannel = _settingsSO.CrossChangedChannel;
    }

    private void OnEnable()
    {
        SetDefaults();
        _popChannel.OnEventRaised += DefaultPopCt;
        _mutChannel.OnEventRaised += DefaultMut;
        _crossChannel.OnEventRaised += DefaultCross;
    }

    private void SetDefaults()
    {
        DefaultPopCt();
        DefaultMut();
        DefaultCross();
    }

    private void DefaultPopCt(int value=0)
    {
        TMP_Dropdown popD = _popDropdown.GetComponent<TMP_Dropdown>();
        switch (_settingsSO.PopulationCount)
        {
            case 16:
                popD.value = 0;
                break;
            case 36:
                popD.value = 1;
                break;
            case 49:
                popD.value = 2;
                break;
            case 64:
                popD.value = 3;
                break;
            case 81:
                popD.value = 4;
                break;
            case 100:
                popD.value = 5;
                break;
            default:
                popD.value = 0;
                break;
        }
    }

    private void DefaultMut(float value = 0)
    {
        Slider mutS = _mutSlider.GetComponent<Slider>();
        mutS.value = _settingsSO.MutationRate;
    }

    private void DefaultCross(float value = 0)
    {
        Slider crossS = _crossSlider.GetComponent<Slider>();
        crossS.value = _settingsSO.CrossoverRate;
    }

    public void PopSubmission(int index)
    {
        switch (index)
        {
            case 0:
                _settingsSO.PopulationCount = 16;
                break;
            case 1:
                _settingsSO.PopulationCount = 36;
                break;
            case 2:
                _settingsSO.PopulationCount = 49;
                break;
            case 3:
                _settingsSO.PopulationCount = 64;
                break;
            case 4:
                _settingsSO.PopulationCount = 81;
                break;
            case 5:
                _settingsSO.PopulationCount = 100;
                break;
            default:
                _settingsSO.PopulationCount = 16;
                break;
        }
    }

    public void MutSubmission(float value)
    {
        _settingsSO.MutationRate = value;
    }

    public void CrossSubmission(float value)
    {
        _settingsSO.CrossoverRate = value;
    }

    private void OnDisable()
    {
        _popChannel.OnEventRaised -= DefaultPopCt;
        _mutChannel.OnEventRaised -= DefaultMut;
        _crossChannel.OnEventRaised -= DefaultCross;
    }
}