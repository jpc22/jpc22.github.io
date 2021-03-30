using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains and manages a list of rooms who have a list of fixtures and fitness for the GA
/// </summary>
public class AlgorithmRoomManager : MonoBehaviour
{
    [SerializeField] private SettingsSO _settingsSO;

    private FixtureContainerSO _selectedFixtureSO;
    private List<GameObject> _roomObjects;
    private List<RoomGA> _roomPop;

    private float _mutRate, _crossRate;
    private FloatEventChannelSO _mutUpdateCh, _crossUpdateCh;
    private bool _paused = true;

    public List<GameObject> RoomObjects
    {
        get => _roomObjects; set
        {
            _roomObjects = value;
        }
    }
    public List<RoomGA> RoomPop { get => _roomPop; }

    private void Awake()
    {
        _selectedFixtureSO = _settingsSO.SelectedFixtureSOList;
        _mutRate = _settingsSO.MutationRate;
        _crossRate = _settingsSO.CrossoverRate;
        _mutUpdateCh = _settingsSO.MutationChangedChannel;
        _crossUpdateCh = _settingsSO.CrossChangedChannel;
    }

    private void OnEnable()
    {
        _mutUpdateCh.OnEventRaised += MutationChanged;
        _crossUpdateCh.OnEventRaised += CrossChanged;
    }
    
    private void MutationChanged(float value)
    {
        _mutRate = value;
    }

    private void CrossChanged(float value)
    {
        _crossRate = value;
    }

    private void OnDisable()
    {
        _mutUpdateCh.OnEventRaised -= MutationChanged;
        _crossUpdateCh.OnEventRaised -= CrossChanged;
    }

    public void UpdateRoomPop()
    {
        int count;
        int newCount = _roomObjects.Count;

        if (_roomPop == null)
        {
            _roomPop = new List<RoomGA>();
            count = 0;
        }
        else count = _roomPop.Count;

        if(newCount > count)
        {
            for (int i = count; i < _roomObjects.Count; i++)
            {
                RoomGA roomGA = _roomObjects[i].AddComponent<RoomGA>();
                _roomPop.Add(roomGA);

                roomGA.Index = i;
                roomGA.Fixtures = _selectedFixtureSO;
                roomGA.Dimensions = new Vector3(_settingsSO.RoomLength, 2.4384f, _settingsSO.RoomWidth);
            }

            for (int i = count; i < _roomPop.Count; i++)
            {
                _roomPop[i].InitializeFixtures();
            }
        }
        else if (newCount < count)
        {
            _roomPop.RemoveRange(newCount, count - newCount);
        }
        else Debug.Log("UpdateRoomPop found no difference in pop count");
    }

    public List<RoomGA> GetBestFitList()
    {
        List<RoomGA> list = new List<RoomGA>();
        var sortedRooms = _roomPop.OrderByDescending(p => p.Fitness);
        foreach (RoomGA room in sortedRooms)
        {
            list.Add(room);
        }
        return list;
    }

    public void StartAlgorithm()
    {
        _paused = false;
        Debug.Log("_paused = false");
    }
    public void PauseAlgorithm()
    {
        _paused = true;
        Debug.Log("_paused = true");
    }
}
