using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScrollListUITest : MonoBehaviour
{
    [SerializeField] private GameObject _fixturePrefab;
    [SerializeField] private FixtureContainerSO _selectedFixturesListSO;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private FixtureSO _roomSO;
    [SerializeField] private FixtureContainerSO _staticFixtures;
    [SerializeField] private SettingsSO _settingsSO;
    [SerializeField] private List<GameObject> _selectPanels;
    [SerializeField] private GameObject _saveWindow;
    [SerializeField] private GameObject _noneSelectedWindow;

    private VoidEventChannelSO _settingChangedChannel;


    void Awake()
    {
        _settingChangedChannel = _settingsSO.SettingsChangedChannel;
        
    }

    private void Start()
    {
        
        if (_settingsSO.UseGA)
        {
            foreach (GameObject obj in _selectPanels)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in _selectPanels)
            {
                if (!obj.activeSelf)
                    obj.SetActive(true);
            }
        }
        
    }

    private void OnEnable()
    {
        _settingChangedChannel.OnEventRaised += SettingsUpdated;
    }

    private void SettingsUpdated()
    {
        
        if (_settingsSO.UseGA)
        {
            foreach (GameObject obj in _selectPanels)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in _selectPanels)
            {
                if (!obj.activeSelf)
                    obj.SetActive(true);
            }
        }
        
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EditRoomPressed()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("EditRoomScene");
    }

    public void StartPressed()
    {
        if (_settingsSO.UseGA)
        {

            PlayerPrefs.Save();
            if (_settingsSO.SelectedFixtureSOList.SizeList.Count > 0)
            {
                SceneManager.LoadScene("AlgorithmScene");
            }
            else
            {
                _noneSelectedWindow.SetActive(true);
            }
        }
        else
        {

            PlayerPrefs.Save();
            SceneManager.LoadScene("ViewScene");
        }

    }

    public void SaveLoadStaticPressed()
    {
        if(!_saveWindow.activeSelf)
        {
            _saveWindow.GetComponent<SaveManager>().Container = _settingsSO.StaticFixtureSOList;
            _saveWindow.SetActive(true);
        }
        else
        {
            _saveWindow.SetActive(false);
            _saveWindow.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
        }
    }

    public void SaveLoadSelectedPressed()
    {
        if (!_saveWindow.activeSelf)
        {
            _saveWindow.GetComponent<SaveManager>().Container = _settingsSO.SelectedFixtureSOList;
            _saveWindow.SetActive(true);
        }
        else
        {
            _saveWindow.SetActive(false);
            _saveWindow.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
        }
    }

    private void OnDisable()
    {
        _settingChangedChannel.OnEventRaised -= SettingsUpdated;
    }
}
