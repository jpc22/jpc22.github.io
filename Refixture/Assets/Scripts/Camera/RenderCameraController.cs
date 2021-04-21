using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RefixUtilities;

public class RenderCameraController : MonoBehaviour
{
    public GameObject content;
    private GameObject _model;
    [SerializeField] SettingsSO _settingsSO;
    VoidEventChannelSO _settingsUpdated;

    private void Awake()
    {
        if (_settingsSO != null)
        {
            _settingsUpdated = _settingsSO.SettingsChangedChannel;
        }
    }

    private void OnEnable()
    {
        if (_settingsUpdated != null)
            _settingsUpdated.OnEventRaised += SettingChanged;
    }

    public GameObject Model
    {
        get => _model; set
        {
            _model = value;
            InitializeModel();
        }
    }

    private void InitializeModel()
    {
        UpdateCameraView();
    }

    public void UpdateCameraView()
    {
        if (Model != null)
        {
            Bounds box = RefixUtility.GetCombinedBoundingBoxOfChildren(_model.transform);
            float magnitude = box.size.magnitude;
            content.transform.localPosition = new Vector3(content.transform.localPosition.x, content.transform.localPosition.y, magnitude + 1f);
            transform.LookAt(_model.transform.GetChild(0).position + new Vector3(0, box.extents.y, 0));
        }
    }

    public void SettingChanged()
    {
        if (_model == null && content.transform.childCount > 0)
        {
            Model = content.transform.GetChild(0).gameObject;
        }
        UpdateCameraView();
    }
    
    void FixedUpdate()
    {
        if (_model == null && content.transform.childCount > 0)
        {
            Model = content.transform.GetChild(0).gameObject;
        }
    }

    private void OnDisable()
    {
        if (_settingsUpdated != null)
            _settingsUpdated.OnEventRaised -= SettingChanged;
    }
}
