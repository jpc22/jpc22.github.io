using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewSave : SaveContent
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private string _inputString;

    public string InputString
    {
        get => _inputString; set
        {
            _inputString = value;
            _fileName = value;
            _fileNameCh.RaiseEvent(value);
        }
    }

    private void Awake()
    {
        _inputString = "Save";
    }

    public override void toggled(bool value)
    {
        base.toggled(value);
        if (value)
        {
            _fileNameTMP.gameObject.SetActive(false);
            _inputField.gameObject.SetActive(true);
        }
        else
        {
            _fileNameTMP.gameObject.SetActive(true);
            _inputField.gameObject.SetActive(false);
        }
    }

}
