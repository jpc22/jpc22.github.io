using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SettingsSO")]
public class SettingsSO : ScriptableObject
{
    [SerializeField] private FixtureContainerSO _selectedFixtureSOList;
    [SerializeField] private FixtureContainerSO _staticFixtureSOList;
    [SerializeField] private bool _useImperial;
    [SerializeField] private bool _useGA;
    [SerializeField] private float _roomWidth;
    [SerializeField] private float _roomLength;
    [SerializeField] private int _populationCount;
    [SerializeField] private float _mutationRate;
    [SerializeField] private float _crossoverRate;

    [SerializeField] private VoidEventChannelSO _settingsChangedChannel;
    [SerializeField] private FloatEventChannelSO _widthChangedChannel;
    [SerializeField] private FloatEventChannelSO _lengthChangedChannel;
    [SerializeField] private IntEventChannelSO _popChangedChannel;
    [SerializeField] private FloatEventChannelSO _mutationChangedChannel;
    [SerializeField] private FloatEventChannelSO _crossChangedChannel;

    private static string _popKey = "PopulationCount";
    private static int _popDefault = 32;
    private static string _mutKey = "MutationRate";
    private static float _mutDefault = 0.1f;
    private static string _crossKey = "CrossoverRate";
    private static float _crossDefault = 0.5f;

    public bool UseImperial
    {
        get => _useImperial; set
        {
            _useImperial = value;
            SettingsChangedChannel.RaiseEvent();
        }
    }

    public bool UseGA
    {
        get => _useGA; set
        {
            _useGA = value;
            SettingsChangedChannel.RaiseEvent();
        }
    }

    public float RoomWidth
    {
        get => _roomWidth; set
        {
            _roomWidth = value;
            _staticFixtureSOList.SetScaleAt(0, _roomLength, 1, _roomWidth);
            WidthChangedChannel.RaiseEvent(value);
            SettingsChangedChannel.RaiseEvent();
        }
    }
    public float RoomLength
    {
        get => _roomLength; set
        {
            _roomLength = value;
            _staticFixtureSOList.SetScaleAt(0, _roomLength, 1, _roomWidth);
            LengthChangedChannel.RaiseEvent(value);
            SettingsChangedChannel.RaiseEvent();
        }
    }
    public int PopulationCount
    {
        get 
        { 
            if (!PlayerPrefs.HasKey(_popKey))
            {
                PlayerPrefs.SetInt(_popKey, _popDefault);
            }
            else
            {
                int keyValue = PlayerPrefs.GetInt(_popKey);
                if (keyValue != _populationCount)
                {
                    _populationCount = keyValue;
                }
            }
            return _populationCount; 
        }
        set
        {
            PlayerPrefs.SetInt(_popKey, value);
            _populationCount = value;
            PopChangedChannel.RaiseEvent(_populationCount);
        }
    }
    public float MutationRate
    {
        get
        {
            if (!PlayerPrefs.HasKey(_mutKey))
            {
                PlayerPrefs.SetFloat(_mutKey, _mutDefault);
            }
            else
            {
                float keyValue = PlayerPrefs.GetFloat(_mutKey);
                if (keyValue != _mutationRate)
                {
                    _mutationRate = keyValue;
                }
            }
            return _mutationRate;
        }
        set
        {
            PlayerPrefs.SetFloat(_mutKey, value);
            _mutationRate = value;
            MutationChangedChannel.RaiseEvent(_mutationRate);
        }
    }
    public float CrossoverRate
    {
        get
        {
            if (!PlayerPrefs.HasKey(_crossKey))
            {
                PlayerPrefs.SetFloat(_crossKey, _crossDefault);
            }
            else
            {
                float keyValue = PlayerPrefs.GetFloat(_crossKey);
                if (keyValue != _crossoverRate)
                {
                    _crossoverRate = keyValue;
                }
            }
            return _crossoverRate;
        }
        set
        {
            PlayerPrefs.SetFloat(_crossKey, value);
            _crossoverRate = value;
            _crossChangedChannel.RaiseEvent(_crossoverRate);
        }
    }

    public FixtureContainerSO SelectedFixtureSOList { get => _selectedFixtureSOList; set => _selectedFixtureSOList = value; }
    public FixtureContainerSO StaticFixtureSOList { get => _staticFixtureSOList; set => _staticFixtureSOList = value; }
    public VoidEventChannelSO SettingsChangedChannel { get => _settingsChangedChannel; set => _settingsChangedChannel = value; }
    public FloatEventChannelSO WidthChangedChannel { get => _widthChangedChannel; set => _widthChangedChannel = value; }
    public FloatEventChannelSO LengthChangedChannel { get => _lengthChangedChannel; set => _lengthChangedChannel = value; }
    public IntEventChannelSO PopChangedChannel { get => _popChangedChannel; set => _popChangedChannel = value; }
    public FloatEventChannelSO MutationChangedChannel { get => _mutationChangedChannel; set => _mutationChangedChannel = value; }
    public FloatEventChannelSO CrossChangedChannel { get => _crossChangedChannel; set => _crossChangedChannel = value; }
}
