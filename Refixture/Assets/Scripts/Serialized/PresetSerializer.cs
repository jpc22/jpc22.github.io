using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class PresetSerializer : MonoBehaviour
{

    void Start()
    {
        string staticPath = Application.persistentDataPath + "/Static Fixtures/";
        string selectedPath = Application.persistentDataPath + "/Selected Fixtures/";

        List<string> staticNames = GetFileNamesFromDirectory(staticPath);
        List<string> selectedNames = GetFileNamesFromDirectory(selectedPath);

        var staticPresets = Resources.LoadAll("StaticPresets", typeof(FixtureContainerSO)).Cast<FixtureContainerSO>().ToList();
        var selectedPresets = Resources.LoadAll("SelectedPresets", typeof(FixtureContainerSO)).Cast<FixtureContainerSO>().ToList();


        WritePresets(staticPresets, staticNames, staticPath);
        WritePresets(selectedPresets, selectedNames, selectedPath);
    }

    private void WritePresets(List<FixtureContainerSO> presets, List<string> fileNames, string path)
    {
        foreach (FixtureContainerSO preset in presets)
        {
            if (!fileNames.Exists(x => x == preset.Name))
            {
                string data = preset.GetJsonString();
                File.WriteAllText(path + preset.Name + ".json", data);
            }
        }
    }
    
    private List<string> GetFileNamesFromDirectory(string path)
    {
        List<string> FileNames = new List<string>();
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string[] fullNames = Directory.GetFiles(path, "*.json");
        foreach (string name in fullNames)
        {
            FileNames.Add(Path.GetFileNameWithoutExtension(name));
        }
        return FileNames;
    }
}
