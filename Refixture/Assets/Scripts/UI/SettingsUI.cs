using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] SettingsSO _settingsSO;
    [SerializeField] GameObject _toggleImperial;
    [SerializeField] GameObject _toggleGA;
    [SerializeField] GameObject _widthInput;
    [SerializeField] GameObject _lengthInput;
    [SerializeField] GameObject _widthPreview;
    [SerializeField] GameObject _lengthPreview;
    [SerializeField] VoidEventChannelSO _settingsUpdateChannel;
    private TMP_InputField _widthTMP;
    private TMP_InputField _lengthTMP;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("UseImperial"))
        {
            _settingsSO.UseImperial = (PlayerPrefs.GetInt("UseImperial") != 0);
        }
        if (PlayerPrefs.HasKey("UseGA"))
        {
            _settingsSO.UseGA = (PlayerPrefs.GetInt("UseGA") != 0);
        }
        _toggleImperial.GetComponent<Toggle>().isOn = _settingsSO.UseImperial;
        _toggleGA.GetComponent<Toggle>().isOn = _settingsSO.UseGA;
        _widthTMP = _widthInput.GetComponent<TMP_InputField>();
        _lengthTMP = _lengthInput.GetComponent<TMP_InputField>();
    }
    void Start()
    {
        SetPreviewWidth();
        SetPreviewLength();
    }

    private void OnEnable()
    {
        _settingsUpdateChannel.OnEventRaised += UpdateSettings;
    }

    public void UpdateSettings()
    {
        SetPreviewLength();
        SetPreviewWidth();
        SetInputText();
    }

    private float convertToInches(float meters) => meters * 39.37007874f;
    private float convertToMeters(float inches) => inches / 39.37007874f;

    void SetPreviewWidth()
    {
        if (_settingsSO.UseImperial == false)
            _widthPreview.GetComponent<TextMeshProUGUI>().text = _settingsSO.RoomWidth.ToString("F2");
        else
            _widthPreview.GetComponent<TextMeshProUGUI>().text = convertToInches(_settingsSO.RoomWidth).ToString("F2");
    }

    void SetPreviewLength()
    {
        if (_settingsSO.UseImperial == false)
            _lengthPreview.GetComponent<TextMeshProUGUI>().text = _settingsSO.RoomLength.ToString("F2");
        else
            _lengthPreview.GetComponent<TextMeshProUGUI>().text = convertToInches(_settingsSO.RoomLength).ToString("F2");
    }
    private void SetInputText()
    {
        if (_settingsSO.UseImperial == true)
        {
            if (_widthTMP.text != "")
                _widthTMP.text = convertToInches(_settingsSO.RoomWidth).ToString("F2");
            if (_lengthTMP.text != "")
                _lengthTMP.text = convertToInches(_settingsSO.RoomLength).ToString("F2");
        }
        else
        {
            if (_widthTMP.text != "")
                _widthTMP.text = _settingsSO.RoomWidth.ToString("F2");
            if (_lengthTMP.text != "")
                _lengthTMP.text = _settingsSO.RoomLength.ToString("F2");
        }
    }
    public void OnWidthInput()
    {
        if (_widthTMP.text != "")
        {
            if (_settingsSO.UseImperial == false)
                _settingsSO.RoomWidth = float.Parse(_widthTMP.text);
            else
                _settingsSO.RoomWidth = convertToMeters(float.Parse(_widthTMP.text));
        }
        else
        {
            SetPreviewWidth();
        }
    }

    public void OnLengthInput()
    {
        if (_lengthTMP.text != "")
        {
            if (_settingsSO.UseImperial == false)
                _settingsSO.RoomLength = float.Parse(_lengthTMP.text);
            else
                _settingsSO.RoomLength = convertToMeters(float.Parse(_lengthTMP.text));
        }
        else
        {
            SetPreviewLength();
        }
    }

    public void ImperialToggled()
    {
        _settingsSO.UseImperial = _toggleImperial.GetComponent<Toggle>().isOn;
        PlayerPrefs.SetInt("UseImperial", (_toggleImperial.GetComponent<Toggle>().isOn ? 1 : 0));
    }

    public void GAToggled()
    {
        _settingsSO.UseGA = _toggleGA.GetComponent<Toggle>().isOn;
        PlayerPrefs.SetInt("UseGa", (_toggleGA.GetComponent<Toggle>().isOn ? 1 : 0));
    }

    private void OnDisable()
    {
        _settingsUpdateChannel.OnEventRaised -= UpdateSettings;
    }
}
