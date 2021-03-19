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
    [SerializeField] private GameObject _resetButton;
    [SerializeField] private FixtureSO _roomSO;
    [SerializeField] private FixtureContainerSO _staticFixtures;
    [SerializeField] private SettingsSO _settingsSO;
    [SerializeField] private List<GameObject> _selectPanels;

    private List<Fixture> _fixtureList = new List<Fixture>();
    private VoidEventChannelSO _settingChangedChannel;

    public bool closeCanvas;

    void Awake()
    {
        _settingChangedChannel = _settingsSO.SettingsChangedChannel;
        
    }

    private void Start()
    {
        /*
        if (_settingsSO.UseGA)
        {
            foreach (GameObject obj in _selectPanels)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in _selectPanels)
            {
                obj.SetActive(false);
            }
        }
        */
    }

    private void OnEnable()
    {
        _settingChangedChannel.OnEventRaised += SettingsUpdated;
    }

    private void SettingsUpdated()
    {
        /*
        if (_settingsSO.UseGA)
        {
            foreach (GameObject obj in _selectPanels)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in _selectPanels)
            {
                obj.SetActive(false);
            }
        }
        */
    }

    public void StartRun()
    {
        if (closeCanvas)
            _canvas.SetActive(false);
        _resetButton.SetActive(true);
        List<FixtureSO> spawnList = _selectedFixturesListSO.Fixtures;
        float x = 0;
        foreach (FixtureSO so in spawnList)
        {
            GameObject fixture = Instantiate(_fixturePrefab);
            Fixture fixtureScript = fixture.GetComponent<Fixture>();
            fixtureScript.FixtureSO = so;
            fixtureScript.Spawn2dFixture();
            fixtureScript.Spawn3dFixture();
            _fixtureList.Add(fixtureScript);

            fixture.transform.position = new Vector3(x, 0);
            x += 2;
            fixtureScript.Transform2d.eulerAngles = new Vector3(90, 0);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EditRoomPressed()
    {
        SceneManager.LoadScene("EditRoomScene");
    }

    public void StartPressed()
    {
        if (_settingsSO.UseGA)
        {
            SceneManager.LoadScene("AlgorithmScene");
        }
        else
        {
            SceneManager.LoadScene("ViewScene");
        }

    }

    private void OnDisable()
    {
        _settingChangedChannel.OnEventRaised -= SettingsUpdated;
    }
}
