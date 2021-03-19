using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPreview : MonoBehaviour
{
    [SerializeField] RenderTexture _renderTexture;
    [SerializeField] FixtureContainerSO _roomObjects;
    [SerializeField] GameObject _previewRenderer;
    [SerializeField] SettingsSO _settingsSO;
    List<GameObject> _previewObjects;
    FloatEventChannelSO _widthCh;
    FloatEventChannelSO _lengthCh;
    GameObject _renderCamera;
    Vector3 _defaultCameraPosition = new Vector3(25, 50, -25);
    int _renderLayer = 8;

    private void Awake()
    {
        _widthCh = _settingsSO.WidthChangedChannel;
        _lengthCh = _settingsSO.LengthChangedChannel;
        _previewObjects = new List<GameObject>();
        StartPreviewRender();
    }

    private void Start()
    {
        
        
    }

    private void FixedUpdate()
    {
        UpdateCameraView();
    }

    private void OnEnable()
    {
        _widthCh.OnEventRaised += UpdateWidth;
        _lengthCh.OnEventRaised += UpdateLength;
    }

    private void UpdateWidth(float width)
    {
        Transform foundation = _renderCamera.transform.GetChild(0).GetChild(0);
        foundation.localScale = new Vector3(foundation.localScale.x, foundation.localScale.y, width);
        UpdateCameraView();
    }
    private void UpdateLength(float length)
    {
        Transform foundation = _renderCamera.transform.GetChild(0).GetChild(0);
        foundation.localScale = new Vector3(length, foundation.localScale.y, foundation.localScale.z);
        UpdateCameraView();
    }

    private void StartPreviewRender()
    {
        // create and position camera
        _renderCamera = Instantiate(_previewRenderer, transform);
        _renderCamera.transform.position = _defaultCameraPosition;

        // set object to render in camera
        Camera cam = _renderCamera.GetComponentInChildren<Camera>();
        cam.targetTexture = _renderTexture;
        for(int i = 0; i < _roomObjects.Fixtures.Count; i++)
        {
            GameObject model = Instantiate(_roomObjects.Fixtures[i].Prefab3d, _renderCamera.transform.GetChild(0));
            model.layer = _renderLayer;
            foreach (var child in model.GetComponentsInChildren(typeof(Transform), true))
            {
                child.gameObject.layer = _renderLayer;
            }

            model.transform.localScale = _roomObjects.ScaleAt(i);
            model.transform.localPosition = _roomObjects.PosAt(i);
            model.transform.localEulerAngles = _roomObjects.RotAt(i);
            _previewObjects.Add(model);
        }
        UpdateCameraView();
    }

    private void UpdateCameraView()
    {
        GameObject foundation = _previewObjects[0];
        if (foundation != null)
        {
            Transform model = _renderCamera.transform.GetChild(0);
            Transform camera = _renderCamera.transform.GetChild(1);
            BoxCollider collider = foundation.GetComponent<BoxCollider>();
            float magnitude = (_settingsSO.RoomWidth + _settingsSO.RoomLength) / 2;
            model.localPosition = new Vector3(0, -4f, magnitude);
            camera.LookAt(model.transform.position + collider.center);
        }
    }

    private void OnDisable()
    {
        _widthCh.OnEventRaised -= UpdateWidth;
        _lengthCh.OnEventRaised -= UpdateLength;
    }
}
