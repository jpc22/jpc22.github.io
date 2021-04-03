using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownControl : MonoBehaviour
{
    [SerializeField] private List<FixtureContainerSO> _dropdowns;
    [SerializeField] private GameObject _scrollViewObj;
    [SerializeField] private Dropdown _dropdown;
    private ScrollView _scrollView;

    public void Awake()
    {
        _scrollView = _scrollViewObj.GetComponent<ScrollView>();
        List<Dropdown.OptionData> optionList = new List<Dropdown.OptionData>();
        for (int i = 0; i < _dropdowns.Count; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = _dropdowns[i].Name;
            optionList.Add(option);
        }
        _dropdown.options = optionList;
    }

    public void OnDropdownChanged(int index)
    {
        _scrollView.FixtureContainer = _dropdowns[index];
    }
}
