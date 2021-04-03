using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationControl : MonoBehaviour
{
    [SerializeField] private SettingsSO _settingsSO;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;

    [SerializeField] private int _selectedIndex;
    [SerializeField] private int _selectedMax;

    private void Start()
    {
        _prevButton.interactable = false;
        _selectedIndex = 0;
        if (_settingsSO.RoomSOList != null)
            _selectedMax = _settingsSO.RoomSOList.Count - 1;
        else
            _selectedMax = 0;

        if (_selectedMax == 0)
            _nextButton.interactable = false;
    }

    public void NextClicked()
    {
        _selectedIndex += 1;
        if (_selectedIndex == _selectedMax)
        {
            _nextButton.interactable = false;
        }

        _prevButton.interactable = true;
    }

    public void PrevClicked()
    {
        _selectedIndex -= 1;
        if (_selectedIndex == 0)
        {
            _prevButton.interactable = false;
        }

        _nextButton.interactable = true;
    }

}
