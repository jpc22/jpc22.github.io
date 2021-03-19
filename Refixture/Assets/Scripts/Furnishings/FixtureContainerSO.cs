using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<Vector3> SizeList { get => new List<Vector3>(_sizeList); set => _sizeList = value; }
    public VoidEventChannelSO UpdateChannel { get => _updateChannel; set => _updateChannel = value; }
    public VoidEventChannelSO AddedChannel { get => _addedChannel; set => _addedChannel = value; }
    public IntEventChannelSO RemovedChannel { get => _removedChannel; set => _removedChannel = value; }

    public void Add(FixtureSO so, float width = 0, float length = 0, float height = 0, float x = 1, float y = 1, float z = 1)
    {
        if (_fixtures == null)
        {
            _fixtures = new List<FixtureSO>();
            RescaleList = new List<Vector3>();
        }

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
        if (_fixtures != null)
        {
            if (index < _fixtures.Count)
            {
                _fixtures.RemoveAt(index);
                _rescaleList.RemoveAt(index);
                _sizeList.RemoveAt(index);
                _posList.RemoveAt(index);
                _rotList.RemoveAt(index);

                if (UpdateChannel != null)
                    UpdateChannel.RaiseEvent();
                if (RemovedChannel != null)
                    RemovedChannel.RaiseEvent(index);
            }
        }
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

}
