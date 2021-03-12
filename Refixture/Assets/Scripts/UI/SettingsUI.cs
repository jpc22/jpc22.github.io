using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] SettingsSO _settingsSO;
    [SerializeField] GameObject _toggleImperial;
    [SerializeField] GameObject _toggleGA;
    [SerializeField] GameObject _widthInput;
    [SerializeField] GameObject _lengthInput;
    [SerializeField] VoidEventChannelSO _settingsUpdateChannel;
    private TMP_InputField _widthTMP;
    private TMP_InputField _lengthTMP;
    private bool _useImperial;

    protected virtual bool UseImperial { get => _useImperial; set => _useImperial = value; }

    private void Awake()
    {
        _settingsUpdateChannel.OnEventRaised += UpdateSettings;
    }
    void Start()
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

    public void UpdateSettings()
    {
        UseImperial = _settingsSO.UseImperial;
    }

    private float convertToInches(float meters) => meters * 39.37007874f;
    private float convertToMeters(float inches) => inches / 39.37007874f;

    public void OnWidthInput()
    {
        if (_widthTMP.text != "")
        {
            _settingsSO.RoomWidth = float.Parse(_widthTMP.text);
        }
    }

    public void OnLengthInput()
    {
        if (_lengthTMP.text != "")
        {
            _settingsSO.RoomLength = float.Parse(_lengthTMP.text);
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
}
