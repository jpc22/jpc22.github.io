using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapControl : MonoBehaviour
{
    [SerializeField] SettingsSO _settingsSO;
    [SerializeField] Toggle _thisToggle;

    private void Awake()
    {
        _thisToggle.isOn = _settingsSO.IsSnapEnabled;
    }

    public void OnToggle(bool value)
    {
        _settingsSO.IsSnapEnabled = value;
    }
}
