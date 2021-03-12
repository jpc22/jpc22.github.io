using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SettingsSO")]
public class SettingsSO : ScriptableObject
{
    [SerializeField] private VoidEventChannelSO _settingsChangedChannel;
    [SerializeField] private FixtureContainerSO _selectedFixtureSOList;
    [SerializeField] private bool _useImperial;
    [SerializeField] private bool _useGA;
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

    public FixtureContainerSO SelectedFixtureSOList { get => _selectedFixtureSOList; set => _selectedFixtureSOList = value; }
}
