using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownControl : MonoBehaviour
{
    [SerializeField] private List<FixtureContainerSO> _dropdowns;
    [SerializeField] private GameObject _scrollViewObj;
    private ScrollView _scrollView;

    public void Awake()
    {
        _scrollView = _scrollViewObj.GetComponent<ScrollView>();
    }

    public void OnDropdownChanged(int index)
    {
        _scrollView.FixtureContainer = _dropdowns[index];
    }
}
