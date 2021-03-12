using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private SettingsSO _settingsSO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Save(string saveName)
    {
        SaveSettings newSave = new SaveSettings();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + ".dat");
        
    }
}

[System.Serializable]
public class SaveSettings
{
    public float roomWidth;
    public float roomHeight;
    public List<FixtureSO> selectionList;
    public List<Vector3> selectionListScales;
}
