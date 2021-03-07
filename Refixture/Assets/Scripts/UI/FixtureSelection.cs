using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FixtureSelection : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerDownHandler
{
    [SerializeField] private GameObject nameText;
    [SerializeField] private GameObject _widthInput;
    [SerializeField] private GameObject _lengthInput;
    [SerializeField] private GameObject _heightInput;
    [SerializeField] private GameObject previewImage;
    [SerializeField] private GameObject _previewRenderer;
    [SerializeField] private FurnishingSO _selection;
    [SerializeField] private string _name;
    [SerializeField] private RenderTexture _previewImageTexture;
    [SerializeField] private SelectedFixtureListSO _furnitureList;
    [SerializeField] private VoidEventChannelSO _settingsUpdatedChannel;
    [SerializeField] private SettingsSO _settingsSO;
    private float _initialWidth;
    private float _initialLength;
    private float _initialHeight;
    private float _widthValue;
    private float _lengthValue;
    private float _heightValue;
    private bool _useImperial;
    private TMP_InputField _widthTMP;
    private TMP_InputField _lengthTMP;
    private TMP_InputField _heightTMP;
    private GameObject _previewModel;
    private bool _isSelected = false;

    void Awake()
    {
        _widthTMP = _widthInput.GetComponent<TMP_InputField>();

        _lengthTMP = _lengthInput.GetComponent<TMP_InputField>();

        _heightTMP = _heightInput.GetComponent<TMP_InputField>();

        UseImperial = _settingsSO.UseImperial;
        _settingsUpdatedChannel.OnEventRaised += OnImperialSettingChanged;
    }

    public FurnishingSO Selection
    {
        get => _selection; set
        {
            _selection = value;
            Name = value.Name;
            PreviewImage = value.PreviewTexture;
            StartPreviewRender();

            BoxCollider collider = _previewModel.GetComponent<BoxCollider>();
            if (collider != null)
            {
                _widthValue = collider.bounds.size.x;
                _lengthValue = collider.bounds.size.z;
                _heightValue = collider.bounds.size.y;

                _initialWidth = _widthValue;
                _initialLength = _lengthValue;
                _initialHeight = _heightValue;

                SetInputPreview();
            }
        }
    }

    public string Name
    {
        get => _name; set
        {
            _name = value;
            nameText.GetComponent<TextMeshProUGUI>().text = value;
        }
    }

    public RenderTexture PreviewImage
    {
        get => _previewImageTexture; set
        {
            _previewImageTexture = value;
            previewImage.GetComponent<RawImage>().texture = value;
        }
    }

    public bool IsSelected => _isSelected;

    public bool UseImperial
    {
        get => _useImperial; set
        {
            _useImperial = value;
            SetInputPreview();
            SetInputText();
        }
    }

    void StartPreviewRender()
    {
        // get Y value of this rect to help position the camera separate from others
        RectTransform thisRect = GetComponent<RectTransform>();
        float thisY = thisRect.anchoredPosition.y;

        // create and position camera
        GameObject renderCamera = Instantiate(_previewRenderer, transform);
        renderCamera.transform.position = new Vector3(renderCamera.transform.position.x, thisY, renderCamera.transform.position.z);

        // set object to render in camera
        Camera cam = renderCamera.GetComponent<Camera>();
        cam.targetTexture = PreviewImage;
        _previewModel = Instantiate(_selection.Prefab3d, renderCamera.transform.GetChild(0), false);

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
    }

    void SetInputPreview()
    {
        TextMeshProUGUI widthUI = _widthTMP.placeholder.gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI lengthUI = _lengthTMP.placeholder.gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI heightUI = _heightTMP.placeholder.gameObject.GetComponent<TextMeshProUGUI>();
        string widthStr, lengthStr, heightStr;
        if (UseImperial == true)
        {
            widthStr = "W: " + convertToInches(_widthValue).ToString("F2");
            lengthStr = "L: " + convertToInches(_lengthValue).ToString("F2");
            heightStr = "H: " + convertToInches(_heightValue).ToString("F2");
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
                _widthTMP.text = convertToInches(_widthValue).ToString("F2");
            if (_lengthTMP.text != "")
                _lengthTMP.text = convertToInches(_lengthValue).ToString("F2");
            if (_heightTMP.text != "")
                _heightTMP.text = convertToInches(_heightValue).ToString("F2");
        }
        else
        {
            if (_widthTMP.text != "")
                _widthTMP.text = _widthValue.ToString("F2");
            if (_lengthTMP.text !="")
                _lengthTMP.text = _lengthValue.ToString("F2");
            if (_heightTMP.text != "")
                _heightTMP.text = _heightValue.ToString("F2");
        }
    }

    private float convertToInches(float meters) => meters * 39.37007874f;
    private float convertToMeters(float inches) => inches / 39.37007874f;

    public void AddButtonPressed()
    {
        AddToList();
    }

    public void AddToList()
    {
        float scaleZ = _widthValue / _initialWidth;
        float scaleX = _lengthValue / _initialLength;
        float scaleY = _heightValue / _initialHeight;
        _furnitureList.Add(_selection, scaleX, scaleY, scaleZ);
    }

    public void OnImperialSettingChanged()
    {
        UseImperial = _settingsSO.UseImperial;
    }
    //////////////////////////////////////////////////////////////
    // Add selection to list when selection UI is clicked twice //
    //
    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsSelected)
            AddToList();
    }
                                                                 //
    ///////////////////////////////////////////////////////////////
    /// Text changed actions
    public void OnSubmission()
    {
        if (UseImperial == true)
        {
            if (_widthTMP.text != "")
                _widthValue = convertToMeters(float.Parse(_widthTMP.text));
            if (_lengthTMP.text != "")
                _lengthValue = convertToMeters(float.Parse(_lengthTMP.text));
            if (_heightTMP.text != "")
                _heightValue = convertToMeters(float.Parse(_heightTMP.text));
        }
        else
        {
            if (_widthTMP.text != "")
                _widthValue = float.Parse(_widthTMP.text);
            if (_lengthTMP.text != "")
                _lengthValue = float.Parse(_lengthTMP.text);
            if (_heightTMP.text != "")
                _heightValue = float.Parse(_heightTMP.text);
        }
        UpdatePreviewRender();
    }
}
