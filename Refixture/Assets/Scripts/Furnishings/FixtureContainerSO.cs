using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

[CreateAssetMenu(menuName = "Fixture/Fixture Object Container")]
public class FixtureContainerSO : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] List<FixtureSO> _fixtures;
    [SerializeField] private List<Vector3> _rescaleList;
    [SerializeField] private List<Vector3> _sizeList;
    [SerializeField] private List<Vector3> _posList;
    [SerializeField] private List<Vector3> _rotList;
    [SerializeField] private VoidEventChannelSO _updateChannel;
    [SerializeField] private VoidEventChannelSO _addedChannel;
    [SerializeField] private IntEventChannelSO _removedChannel;

    public string Name => _name;
    /// <summary>get returns copy</summary>
    public List<FixtureSO> Fixtures { get => new List<FixtureSO>(_fixtures); set => _fixtures = value; }
    /// <summary>get returns copy</summary>
    public List<Vector3> RescaleList { get => new List<Vector3>(_rescaleList); set => _rescaleList = value; }
    /// <summary>get returns copy</summary>
    public List<Vector3> SizeList { get => new List<Vector3>(_sizeList); set => _sizeList = value; }
    /// <summary>get returns copy</summary>
    public List<Vector3> PosList { get => new List<Vector3>(_posList); set => _posList = value; }
    /// <summary>get returns copy</summary>
    public List<Vector3> RotList { get => new List<Vector3>(_rotList); set => _rotList = value; }
    public VoidEventChannelSO UpdateChannel { get => _updateChannel; set => _updateChannel = value; }
    public VoidEventChannelSO AddedChannel { get => _addedChannel; set => _addedChannel = value; }
    public IntEventChannelSO RemovedChannel { get => _removedChannel; set => _removedChannel = value; }

    public void Add(FixtureSO so, float width = 0, float length = 0, float height = 0, float x = 1, float y = 1, float z = 1)
    {
        if (_fixtures == null)
            _fixtures = new List<FixtureSO>();
        if (_rescaleList == null)
            RescaleList = new List<Vector3>();
        if (_sizeList == null)
            _sizeList = new List<Vector3>();
        if (_posList == null)
            _posList = new List<Vector3>();
        if (_rotList == null)
            _rotList = new List<Vector3>();

        _fixtures.Add(so);
        _rescaleList.Add(new Vector3(x, y, z));
        _sizeList.Add(new Vector3(length, height, width));
        _posList.Add(new Vector3(0, 0));
        _rotList.Add(new Vector3(0, 0));

        if(UpdateChannel != null)
            UpdateChannel.RaiseEvent();
        if (AddedChannel != null)
            AddedChannel.RaiseEvent();
    }

    public void Remove(int index)
    {
        if (_fixtures != null && index < _fixtures.Count)
            _fixtures.RemoveAt(index);
        if (_rescaleList != null && index < _rescaleList.Count)
            _rescaleList.RemoveAt(index);
        if (_sizeList != null && index < _sizeList.Count)
            _sizeList.RemoveAt(index);
        if (_posList != null && index < _posList.Count)
            _posList.RemoveAt(index);
        if (_rotList != null && index < _rotList.Count)
            _rotList.RemoveAt(index);

        if (UpdateChannel != null)
            UpdateChannel.RaiseEvent();
        if (RemovedChannel != null)
            RemovedChannel.RaiseEvent(index);
    }

    public Vector3 ScaleAt(int index)
    {
        if (_rescaleList != null)
            return new Vector3(_rescaleList[index].x, _rescaleList[index].y, _rescaleList[index].z);
        else
            return new Vector3(1f, 1f, 1f);
    }
    public void SetScaleAt(int index, float x, float y, float z)
    {
        if (_rescaleList != null)
            _rescaleList[index] = new Vector3(x, y, z);
    }
    public Vector3 SizeAt(int index)
    {
        if (_sizeList != null)
            return new Vector3(_sizeList[index].x, _sizeList[index].y, _sizeList[index].z);
        else
            return new Vector3(1f, 1f, 1f);
    }
    public Vector3 PosAt(int index)
    {
        if (_posList != null)
            return new Vector3(_posList[index].x, _posList[index].y, _posList[index].z);
        else
            return new Vector3(0f, 0f, 0f);
    }
    public void SetPosAt(int index, float x, float y, float z)
    {
        if (_posList != null)
            _posList[index] = new Vector3(x, y, z);
    }
    public Vector3 RotAt(int index)
    {
        if (_rotList != null)
            return new Vector3(_rotList[index].x, _rotList[index].y, _rotList[index].z);
        else
            return new Vector3(0f, 0f, 0f);
    }
    public void SetRotAt(int index, float x, float y, float z)
    {
        if (_rotList != null)
            _rotList[index] = new Vector3(x, y, z);
    }

    public void AppendWith(FixtureContainerSO other)
    {
        _fixtures.AddRange(other.Fixtures);
        _rescaleList.AddRange(other.RescaleList);
        _sizeList.AddRange(other.SizeList);
        _posList.AddRange(other.PosList);
        _rotList.AddRange(other.RotList);
    }

    public void Copy(FixtureContainerSO other)
    {
        _fixtures = other.Fixtures;
        _rescaleList = other.RescaleList;
        _sizeList = other.SizeList;
        _posList = other.PosList;
        _rotList = other.RotList;
    }

    public void Copy(FixtureContainerSO other, int index)
    {
        _fixtures[index] = other._fixtures[index];
        _rescaleList[index] = other.RescaleList[index];
        _sizeList[index] = other.SizeList[index];
        _posList[index] = other.PosList[index];
        _rotList[index] = other.RotList[index];
    }

    public string GetJsonString()
    {
        FixtureContainer save = new FixtureContainer();
        save.fixtures.AddRange(_fixtures.Select(fixture => fixture.name));
        save.scales = _rescaleList;
        save.sizes = _sizeList;
        save.positions = _posList;
        save.rotations = _rotList;
        if (_updateChannel != null)
            save.update = _updateChannel.name;
        if (_addedChannel != null)
            save.added = _addedChannel.name;
        if (_removedChannel != null)
            save.removed = _removedChannel.name;

        return JsonConvert.SerializeObject(save, Formatting.Indented);
    }

    public void LoadJsonString(string jsonData)
    {
        FixtureContainer save = JsonConvert.DeserializeObject<FixtureContainer>(jsonData);
        _fixtures = new List<FixtureSO>();
        foreach (string name in save.fixtures)
        {
            _fixtures.Add(Resources.Load<FixtureSO>(name));
        }
        _rescaleList = save.scales;
        _sizeList = save.sizes;
        _posList = save.positions;
        _rotList = save.rotations;
        if (save.update != null)
            _updateChannel = Resources.Load<VoidEventChannelSO>(save.update);
        if (save.added != null)
            _addedChannel = Resources.Load<VoidEventChannelSO>(save.added);
        if (save.removed != null)
            _removedChannel = Resources.Load<IntEventChannelSO>(save.removed);
    }

    

}


public class FixtureContainer
{
    public List<string> fixtures;
    public List<Vector3> scales;
    public List<Vector3> sizes;
    public List<Vector3> positions;
    public List<Vector3> rotations;
    public string update;
    public string added;
    public string removed;

    public FixtureContainer()
    {
        fixtures = new List<string>();
    }
}
