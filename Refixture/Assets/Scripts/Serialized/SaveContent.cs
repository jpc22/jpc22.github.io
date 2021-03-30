using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveContent : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _fileNameTMP;
    [SerializeField] protected string _fileName;
    [SerializeField] protected StringEventChannelSO _fileNameCh;

    public string FileName
    {
        get => _fileName; set
        {
            _fileNameTMP.text = value;
            _fileName = value;
        }
    }

    public virtual void toggled(bool value)
    {
        if(value)
        {
            _fileNameCh.RaiseEvent(_fileName);
        }
    }
}
