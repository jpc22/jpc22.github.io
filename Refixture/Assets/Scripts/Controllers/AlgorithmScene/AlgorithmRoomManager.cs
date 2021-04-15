using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RefixUtilities;

/// <summary>
/// Contains and manages a list of rooms who have a list of fixtures and fitness for the GA
/// </summary>
[RequireComponent(typeof(AlgorithmRunner))]
public class AlgorithmRoomManager : MonoBehaviour
{
    [SerializeField] private SettingsSO _settingsSO;
    private AlgorithmRunner _runner;

    private FixtureContainerSO _selectedFixtureSO;
    private List<GameObject> _roomObjects;
    private List<RoomGA> _roomPop;

    private float _mutRate, _crossRate;
    private FloatEventChannelSO _mutUpdateCh, _crossUpdateCh;
    [SerializeField] private FloatEventChannelSO _avgFitCh;
    [SerializeField] private IntEventChannelSO _genCtCh;
    private bool _paused = true;

    [SerializeField] private float _avgFitness;
    [SerializeField] private int _generationCount;

    public List<GameObject> RoomObjects
    {
        get => _roomObjects; set
        {
            _roomObjects = value;
        }
    }
    public List<RoomGA> RoomPop { get => _roomPop; }
    public float AvgFitness
    {
        get => _avgFitness; set
        {
            _avgFitness = value;
            _avgFitCh.RaiseEvent(value);
        }
    }

    public int GenerationCount
    {
        get => _generationCount; set
        {
            _generationCount = value;
            _genCtCh.RaiseEvent(value);
        }
    }

    private void Awake()
    {
        _runner = GetComponent<AlgorithmRunner>();
        _selectedFixtureSO = _settingsSO.SelectedFixtureSOList;
        _mutRate = _settingsSO.MutationRate;
        _crossRate = _settingsSO.CrossoverRate;
        _mutUpdateCh = _settingsSO.MutationChangedChannel;
        _crossUpdateCh = _settingsSO.CrossChangedChannel;
        AvgFitness = 0;
        GenerationCount = 0;
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

    public List<FixtureContainerSO> GetBestFitList()
    {
        /*
        List<RoomGA> list = new List<RoomGA>();
        var sortedRooms = _roomPop.OrderByDescending(p => p.Fitness);
        foreach (RoomGA room in sortedRooms)
        {
            list.Add(room);
        }
        */
        List<FixtureContainerSO> best = _runner.Best;
        if (best == null)
            best = new List<FixtureContainerSO>();
        return best;
    }

    public void StartAlgorithm()
    {
        if (_paused == true)
        {
            _paused = false;
            //Debug.Log("unpaused");
            CalculateFitness();
        }
    }
    private void GenerationFinished()
    {
        //Debug.Log("New Generation created");
        CalculateFitness();
    }
    private void NewGeneration()
    {
        if (_paused == false)
        {
            GenerationCount++;
            _runner.StartNewGeneration(_roomPop, () => GenerationFinished());
            //Debug.Log("New Generation Started");
        }
    }
    private void CalculateFitness()
    {
        CallbackCounter cbct = new CallbackCounter(_roomPop.Count, () => FitnessCalculated());
        //Debug.Log("Fitness calculating...");
        foreach (RoomGA room in _roomPop)
        {
            room.CalculateFitness(cbct.Callback);
        }
    }
    private void FitnessCalculated()
    {

        AvgFitness = (_roomPop.Sum(room => room.Fitness) / _roomPop.Count);
        //Debug.Log("Fitness Calculated. Avg Fit = " + AvgFitness);
        Invoke("NewGeneration", 0.5f);
    }
    public void PauseAlgorithm()
    {
        if (_paused == false)
        {
            _paused = true;
            //Debug.Log("paused");
        }
    }
}


