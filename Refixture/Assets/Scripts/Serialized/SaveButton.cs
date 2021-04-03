using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    [SerializeField] SaveManager _saveManager;

    public void ButtonPressed()
    {
        if (!_saveManager.gameObject.activeSelf)
        {
            _saveManager.gameObject.SetActive(true);
        }
        else
        {
            _saveManager.gameObject.SetActive(false);
        }
    }
}
