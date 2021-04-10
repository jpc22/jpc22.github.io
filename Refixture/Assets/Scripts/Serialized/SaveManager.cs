using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private SettingsSO _settingsSO;
    [SerializeField] private FixtureContainerSO _container;
    [SerializeField] private TextMeshProUGUI _titleTMP;
    [SerializeField] private TMP_InputField _pathTMP;
    [SerializeField] private GameObject _toggleGroup;
    [SerializeField] private GameObject _saveContentPrefab;
    [SerializeField] private GameObject _newSavePrefab;

    [SerializeField] protected StringEventChannelSO _fileNameCh;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private GameObject _confirmWindow;
    [SerializeField] private TextMeshProUGUI _confirmTMP;

    private string _fileName;
    private string _filePath;
    private List<string> _fileNames;
    private List<GameObject> _contentObjects;


    public FixtureContainerSO Container { get => _container; set => _container = value; }
    public string FileName { get => _fileName; set => _fileName = value; }
    public bool SaveInteractable { get => _saveButton.interactable; set => _saveButton.interactable = value; }
    public bool LoadInteractable { get => _loadButton.interactable; set => _loadButton.interactable = value; }
    public string TitleText { get => _titleTMP.text; set => _titleTMP.text = value; }

    private void Awake()
    {
        _fileName = "";
    }

    private void OnEnable()
    {
        _titleTMP.text = "Save / Load " + _container.Name;
        _filePath = Application.persistentDataPath + "/" + _container.Name + "/";
        _pathTMP.text = _filePath;
        UpdateFileNamesFromDirectory();
        _fileNameCh.OnEventRaised += FileNameSelected;
    }

    private void OnDisable()
    {
        _fileNameCh.OnEventRaised -= FileNameSelected;
    }

    private void FileNameSelected(string value)
    {
        FileName = value;
    }

    private void UpdateFileNamesFromDirectory()
    {
        _fileNames = new List<string>();
        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }
        string[] fileNames = Directory.GetFiles(_filePath, "*.json");
        foreach(string name in fileNames)
        {
            _fileNames.Add(Path.GetFileNameWithoutExtension(name));
        }
        PopulateScrollList();
    }

    private void PopulateScrollList()
    {
        if (_contentObjects != null)
            UnpopulateScrollList();

        _contentObjects = new List<GameObject>();
        float yOffset = 0;
        foreach (string name in _fileNames)
        {
            GameObject saveContent = Instantiate(_saveContentPrefab, _toggleGroup.transform);
            saveContent.GetComponent<Toggle>().group = _toggleGroup.GetComponent<ToggleGroup>();
            saveContent.GetComponent<SaveContent>().FileName = name;
            _contentObjects.Add(saveContent);

            RectTransform rect = saveContent.GetComponent<RectTransform>();

            Vector3 pos = rect.anchoredPosition;
            pos.y = yOffset;
            rect.anchoredPosition = pos;

            yOffset -= rect.rect.height;
        }

        GameObject newSave = Instantiate(_newSavePrefab, _toggleGroup.transform);
        newSave.GetComponent<Toggle>().group = _toggleGroup.GetComponent<ToggleGroup>();
        _contentObjects.Add(newSave);
        RectTransform newRect = newSave.GetComponent<RectTransform>();

        Vector3 newPos = newRect.anchoredPosition;
        newPos.y = yOffset;
        newRect.anchoredPosition = newPos;

        yOffset -= newRect.rect.height;

        RectTransform groupRect = _toggleGroup.GetComponent<RectTransform>();

        Vector2 size = groupRect.sizeDelta;
        size.y = Mathf.Abs(yOffset);
        groupRect.sizeDelta = size;
    }

    private void UnpopulateScrollList()
    {
        foreach(GameObject obj in _contentObjects)
        {
            Destroy(obj);
        }
    }

    private void Save(string saveName)
    {
        string data = _container.GetJsonString();
        File.WriteAllText(_filePath + saveName + ".json", data);
        UpdateFileNamesFromDirectory();
    }

    private void Delete(string saveName)
    {
        File.Delete(_filePath + saveName + ".json");
        UpdateFileNamesFromDirectory();
    }

    private void Load(string saveName)
    {
        string retrievedData = File.ReadAllText(_filePath + saveName + ".json");
        _container.LoadJsonString(retrievedData);
        if(_container.Name == "Static Fixtures")
        {
            UpdateRoomDimensions();
        }
        VoidEventChannelSO updateCh = _container.UpdateChannel;
        if (updateCh != null)
            updateCh.RaiseEvent();
    }

    private void UpdateRoomDimensions()
    {
        Vector3 foundationScale = _container.ScaleAt(0);
        _settingsSO.RoomLength = foundationScale.x;
        _settingsSO.RoomWidth = foundationScale.z;
    }

    private void ShowConfirmationWindow()
    {
        _confirmWindow.SetActive(true);
        _confirmTMP.text = "Are you sure you want to overwrite " + _fileName + "?";
    }

    public void XPressed()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
    }

    public void SavePressed()
    {
        if (_fileName != "" && !_confirmWindow.activeSelf)
        {
            if (_fileNames.Exists(x => x == _fileName))
            {
                ShowConfirmationWindow();
            }
            else
            {
                Save(_fileName);
            }
        }
    }

    public void DeletePressed()
    {
        if (_fileName != "" && !_confirmWindow.activeSelf)
        {
            if (_fileNames.Exists(x => x == _fileName))
            {
                // Delete found 
                Delete(_fileName);
            }
        }
    }

    public void LoadPressed()
    {
        if (_fileName != "" && !_confirmWindow.activeSelf)
        {
            if (_fileNames.Exists(x => x == _fileName))
            {
                // Load found
                Load(_fileName);
            }
        }
    }

    public void ConfirmPressed()
    {
        if (_fileName != "")
        {
            Save(_fileName);
        }
        _confirmWindow.SetActive(false);
    }

    public void CancelPressed()
    {
        _confirmWindow.SetActive(false);
    }
}