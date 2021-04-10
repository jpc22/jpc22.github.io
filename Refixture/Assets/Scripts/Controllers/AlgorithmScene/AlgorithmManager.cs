using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Takes the static room fixtures and instantiates and positions them. Also serves as manager for UI buttons pressed.
/// </summary>
[RequireComponent(typeof(AlgorithmRoomManager))]
public class AlgorithmManager : MonoBehaviour
{
    [SerializeField] private SettingsSO _settingsSO;
    [SerializeField] private BoolEventChannelSO _startToggleChannel;
    [SerializeField] private TMP_Dropdown _dropdownTMP;

    private AlgorithmRoomManager _roomManager;
    private GameObject _roomContainer;
    private GameObject _roomTemplate;
    private List<GameObject> _rooms;

    private IntEventChannelSO _popUpdateCh;

    private int _currentPop;

    private void Awake()
    {
        _roomManager = gameObject.GetComponent<AlgorithmRoomManager>();
        _popUpdateCh = _settingsSO.PopChangedChannel;
        _currentPop = 0;
    }

    private void OnEnable()
    {
        _popUpdateCh.OnEventRaised += PopUpdated;
        _startToggleChannel.OnEventRaised += StartToggled;
    }

    private void Update()
    {
        if (_roomContainer == null)
        {
            SpawnRooms(_settingsSO.PopulationCount);
        }
    }

    private void PopUpdated(int val)
    {
        //Debug.Log("new pop: " + val);
        if (val > _currentPop)
            SpawnRooms(val);
        else if (val < _currentPop)
            DestroyRooms(val);
    }

    private void DestroyRooms(int newPop)
    {
        for (int i = newPop; i < _currentPop; i++)
        {
            Destroy(_rooms[i]);
        }
        _rooms.RemoveRange(newPop, _currentPop - newPop);
        _currentPop = newPop;
        PositionRooms();

        //send references to the room manager
        _roomManager.RoomObjects = _rooms;
        _roomManager.UpdateRoomPop();
    }

    private void SpawnRooms(int newPop)
    {
        if (_rooms == null)
            _rooms = new List<GameObject>();

        for (int i = _currentPop; i < newPop; i++)
        {
            SpawnRoom(i);
        }
        _currentPop = newPop;
        PositionRooms();

        
        //send references to the room manager
        _roomManager.RoomObjects = _rooms;
        _roomManager.UpdateRoomPop();
    }

    private void CreateRoomTemplate()
    {
        FixtureContainerSO statics = _settingsSO.StaticFixtureSOList;
        List<FixtureSO> prefabs = statics.Fixtures;
        _roomTemplate = new GameObject("RoomTemplate");
        for (int i = 0; i < prefabs.Count; i++)
        {
            GameObject fixture = Instantiate(prefabs[i].GetPrefab3d(), _roomTemplate.transform);
            fixture.transform.localScale = statics.ScaleAt(i);
            fixture.transform.localPosition = statics.PosAt(i);
            fixture.transform.localEulerAngles = statics.RotAt(i);
            Fixture fixtureScript = fixture.AddComponent<Fixture>();
            fixtureScript.FixtureSO = prefabs[i];
        }
        _roomTemplate.SetActive(false);
    }

    private void SpawnRoom(int number)
    {
        if (_roomTemplate == null)
            CreateRoomTemplate();
        if (_roomContainer == null)
            _roomContainer = new GameObject("Rooms");
        GameObject roomInstance = Instantiate(_roomTemplate, _roomContainer.transform);
        roomInstance.SetActive(true);
        roomInstance.name = "Room " + (number + 1);
        _rooms.Add(roomInstance);
    }

    private void PositionRooms()
    {
        int count = _rooms.Count;
        float x = 0;
        float z = 0;
        float xStep = _settingsSO.RoomLength * 2f;
        float zStep = _settingsSO.RoomWidth * 2f;
        int index = 0;
        for (int i = 0; i < Mathf.Sqrt(count); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(count); j++)
            {
                _rooms[index].transform.position = new Vector3(x, 0, z);
                index++;
                x += xStep;
            }
            x = 0;
            z += zStep;
        }
    }

    public void ViewRoomPressed()
    {
        PlayerPrefs.Save();
        SaveGAResults();
        SceneManager.LoadScene("ViewResultScene");
    }

    public void SaveGAResults()
    {
        List<FixtureContainerSO> roomList = _roomManager.GetBestFitList();
        List<FixtureContainerSO> roomSOList = new List<FixtureContainerSO>();
        foreach (FixtureContainerSO room in roomList)
        {
            FixtureContainerSO roomSO = Instantiate(_settingsSO.StaticFixtureSOList);
            roomSO.AppendWith(room);
            roomSOList.Add(roomSO);
        }
        _settingsSO.RoomSOList = roomSOList;
    }

    public void StartToggled(bool value)
    {
        _dropdownTMP.interactable = !value;
        if (value)
        {
            _roomManager.StartAlgorithm();
        }
        else
        {
            _roomManager.PauseAlgorithm();
        }
    }

    private void OnDisable()
    {
        _popUpdateCh.OnEventRaised -= PopUpdated;
        _startToggleChannel.OnEventRaised -= StartToggled;
    }
}
