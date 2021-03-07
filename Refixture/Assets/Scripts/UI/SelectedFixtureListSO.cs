using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lists/Selected Fixture List")]
public class SelectedFixtureListSO : ScriptableObject
{
    [SerializeField] private List<FurnishingSO> _selectedList;
    [SerializeField] private List<Vector3> _rescaleList;
    [SerializeField] private VoidEventChannelSO _updateChannel;

    public List<FurnishingSO> SelectedList {set => _selectedList = value; }

    public void Add(FurnishingSO so, float x = 1, float y = 1, float z = 1)
    {
        if (_selectedList == null)
        {
            _selectedList = new List<FurnishingSO>();
            _rescaleList = new List<Vector3>();
        }
            
        _selectedList.Add(so);
        _rescaleList.Add(new Vector3(x, y, z));
        _updateChannel.RaiseEvent();
    }

    public void Remove(int index)
    {
        if(_selectedList != null)
        {
            if (index < _selectedList.Count)
            {
                _selectedList.RemoveAt(index);
                _rescaleList.RemoveAt(index);
                _updateChannel.RaiseEvent();
            }
        }
    }

    public List<FurnishingSO> getCopy()
    {
        return new List<FurnishingSO>(_selectedList);
    }

    public List<Vector3> getRescaleList()
    {
        return new List<Vector3>(_rescaleList);
    }
}
