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
    [SerializeField] private GameObject _toggleGroup;
    [SerializeField] private GameObject _saveContentPrefab;
    [SerializeField] private GameObject _newSavePrefab;

    [SerializeField] protected StringEventChannelSO _fileNameCh;

    private string _fileName;
    private string _filePath;
    private List<string> _fileNames;
    private List<GameObject> _contentObjects;


    public FixtureContainerSO Container { get => _container; set => _container = value; }
    public string FileName { get => _fileName; set => _fileName = value; }

    private void Awake()
    {
        _fileName = "";
    }

    private void OnEnable()
    {
        _titleTMP.text = "Save / Load " + _container.Name;
        _filePath = Application.persistentDataPath + "/" + _container.Name + "/";
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
        string data = JsonUtility.ToJson(_container, true);
        File.WriteAllText(_filePath + saveName + ".json", data);
    }

    private void Delete(string saveName)
    {
        File.Delete(_filePath + saveName + ".json");
    }

    private void Load(string saveName)
    {
        string retrievedData = File.ReadAllText(_filePath + saveName + ".json");
        JsonUtility.FromJsonOverwrite(retrievedData, _container);
        VoidEventChannelSO updateCh = _container.UpdateChannel;
        if (updateCh != null)
            updateCh.RaiseEvent();
    }

    public void XPressed()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
    }

    public void SavePressed()
    {
        if (_fileName != "")
        {
            // save with _fileName
            Save(_fileName);

            UpdateFileNamesFromDirectory();
        }
    }

    public void DeletePressed()
    {
        if (_fileName != "")
        {
            if (_fileNames.Exists(x => x == _fileName))
            {
                // Delete found 
                Delete(_fileName);
            }
            UpdateFileNamesFromDirectory();
        }
    }

    public void LoadPressed()
    {
        if (_fileName != "")
        {
            if (_fileNames.Exists(x => x == _fileName))
            {
                // Load found
                Load(_fileName);
            }
        }
    }
}

