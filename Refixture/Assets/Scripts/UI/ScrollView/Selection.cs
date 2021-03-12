using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Selection : ScrollContent
{
    [SerializeField] private GameObject _widthInput;
    [SerializeField] private GameObject _lengthInput;
    [SerializeField] private GameObject _heightInput;
    [SerializeField] private GameObject _previewImage;
    [SerializeField] private GameObject _previewRenderer;
    private float _initialWidth;
    private float _initialLength;
    private float _initialHeight;
    private float _widthValue;
    private float _lengthValue;
    private float _heightValue;
    private TMP_InputField _widthTMP;
    private TMP_InputField _lengthTMP;
    private TMP_InputField _heightTMP;
    private RenderTexture _previewImageTexture;
    private GameObject _previewModel;
    private GameObject _renderCamera;

    protected override bool UseImperial
    {
        get => base.UseImperial; set
        {
            base.UseImperial = value;
            SetInputPreview();
            SetInputText();
        }
    }

    protected RenderTexture PreviewImage
    {
        get => _previewImageTexture; set
        {
            _previewImageTexture = value;
            _previewImage.GetComponent<RawImage>().texture = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _widthTMP = _widthInput.GetComponent<TMP_InputField>();
        _lengthTMP = _lengthInput.GetComponent<TMP_InputField>();
        _heightTMP = _heightInput.GetComponent<TMP_InputField>();
    }

    protected override void UpdateSettings()
    {
        base.UpdateSettings();
    }

    protected override void InitializeContent()
    {
        base.InitializeContent();
        PreviewImage = ContentSO.PreviewTexture;
        StartPreviewRender();

        BoxCollider collider = _previewModel.GetComponent<BoxCollider>();
        if (collider != null)
        {
            _widthValue = collider.size.z;
            _lengthValue = collider.size.x;
            _heightValue = collider.size.y;

            _initialWidth = _widthValue;
            _initialLength = _lengthValue;
            _initialHeight = _heightValue;

            SetInputPreview();
        }
    }

    protected void StartPreviewRender()
    {
        // get Y value of this rect to help position the camera separate from others
        RectTransform thisRect = GetComponent<RectTransform>();
        float thisY = thisRect.anchoredPosition.y;

        // create and position camera
        _renderCamera = Instantiate(_previewRenderer, transform);
        _renderCamera.transform.position = new Vector3(_renderCamera.transform.position.x, thisY, _renderCamera.transform.position.z);

        // set object to render in camera
        Camera cam = _renderCamera.GetComponentInChildren<Camera>();
        cam.targetTexture = PreviewImage;
        _previewModel = Instantiate(ContentSO.Prefab3d, _renderCamera.transform.GetChild(0), false);

        int renderLayer = 8; // render layer for these cameras
        _previewModel.layer = renderLayer;
        // match render layer for children objects
        foreach (var child in _previewModel.GetComponentsInChildren(typeof(Transform), true))
        {
            child.gameObject.layer = renderLayer;
        }
    }
    void UpdatePreviewRender()
    {
        float scaleZ = _widthValue / _initialWidth;
        float scaleX = _lengthValue / _initialLength;
        float scaleY = _heightValue / _initialHeight;
        _previewModel.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        _renderCamera.GetComponentInChildren<RenderCameraController>().UpdateCameraView();
    }

    void SetInputPreview()
    {
        TextMeshProUGUI widthUI = _widthTMP.placeholder.gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI lengthUI = _lengthTMP.placeholder.gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI heightUI = _heightTMP.placeholder.gameObject.GetComponent<TextMeshProUGUI>();
        string widthStr, lengthStr, heightStr;
        if (UseImperial == true)
        {
            widthStr = "W: " + ConvertToInches(_widthValue).ToString("F2");
            lengthStr = "L: " + ConvertToInches(_lengthValue).ToString("F2");
            heightStr = "H: " + ConvertToInches(_heightValue).ToString("F2");
        }
        else
        {
            widthStr = "W: " + _widthValue.ToString("F2");
            lengthStr = "L: " + _lengthValue.ToString("F2");
            heightStr = "H: " + _heightValue.ToString("F2");
        }
        widthUI.text = widthStr;
        lengthUI.text = lengthStr;
        heightUI.text = heightStr;
    }
    void SetInputText()
    {
        if (UseImperial == true)
        {
            if (_widthTMP.text != "")
                _widthTMP.text = ConvertToInches(_widthValue).ToString("F2");
            if (_lengthTMP.text != "")
                _lengthTMP.text = ConvertToInches(_lengthValue).ToString("F2");
            if (_heightTMP.text != "")
                _heightTMP.text = ConvertToInches(_heightValue).ToString("F2");
        }
        else
        {
            if (_widthTMP.text != "")
                _widthTMP.text = _widthValue.ToString("F2");
            if (_lengthTMP.text != "")
                _lengthTMP.text = _lengthValue.ToString("F2");
            if (_heightTMP.text != "")
                _heightTMP.text = _heightValue.ToString("F2");
        }
    }

    protected override void OnDoubleSelect(PointerEventData eventData)
    {
        AddToList();
    }

    public virtual void AddToList()
    {
        float scaleZ = _widthValue / _initialWidth;
        float scaleX = _lengthValue / _initialLength;
        float scaleY = _heightValue / _initialHeight;
        _settingsSO.SelectedFixtureSOList.Add(ContentSO, _widthValue, _lengthValue, _heightValue, scaleX, scaleY, scaleZ);
    }

    public void OnSubmission()
    {
        if (UseImperial == true)
        {
            if (_widthTMP.text != "")
                _widthValue = ConvertToMeters(float.Parse(_widthTMP.text));
            else _widthValue = _initialWidth;

            if (_lengthTMP.text != "")
                _lengthValue = ConvertToMeters(float.Parse(_lengthTMP.text));
            else _lengthValue = _initialLength;

            if (_heightTMP.text != "")
                _heightValue = ConvertToMeters(float.Parse(_heightTMP.text));
            else _heightValue = _initialHeight;
        }
        else
        {
            if (_widthTMP.text != "")
                _widthValue = float.Parse(_widthTMP.text);
            else _widthValue = _initialWidth;

            if (_lengthTMP.text != "")
                _lengthValue = float.Parse(_lengthTMP.text);
            else _lengthValue = _initialLength;

            if (_heightTMP.text != "")
                _heightValue = float.Parse(_heightTMP.text);
            else _heightValue = _initialHeight;
        }
        UpdatePreviewRender();
    }
}
