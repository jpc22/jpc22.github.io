using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SettingsSO")]
public class SettingsSO : ScriptableObject
{
    [SerializeField] private VoidEventChannelSO _settingsChangedChannel;
    [SerializeField] private bool _useImperial = false;
    [SerializeField] private bool _useGA = false;
    [SerializeField] private float _roomWidth;
    [SerializeField] private float _roomLength;

    public bool UseImperial
    {
        get => _useImperial; set
        {
            _useImperial = value;
            _settingsChangedChannel.RaiseEvent();
        }
    }

    public bool UseGA
    {
        get => _useGA; set
        {
            _useGA = value;
            _settingsChangedChannel.RaiseEvent();
        }
    }

    public float RoomWidth
    {
        get => _roomWidth; set
        {
            _roomWidth = value;
            _settingsChangedChannel.RaiseEvent();
        }
    }
    public float RoomLength
    {
        get => _roomLength; set
        {
            _roomLength = value;
            _settingsChangedChannel.RaiseEvent();
        }
    }

    public void toggleUseImperial()
    {
        _useImperial = !_useImperial;
        _settingsChangedChannel.RaiseEvent();
    }

    public void toggleUseGA()
    {
        UseGA = !UseGA;
        _settingsChangedChannel.RaiseEvent();
    }
}
