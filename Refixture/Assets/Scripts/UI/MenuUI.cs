using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _aboutWindow;

    public void CreateButtonPressed()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void ViewButtonPressed()
    {
        SceneManager.LoadScene("ViewScene");
    }

    public void FeedbackButtonPressed()
    {
        Application.OpenURL("https://www.google.com/");
    }

    public void AboutButtonPressed()
    {
        if (!_aboutWindow.activeSelf)
        {
            _aboutWindow.SetActive(true);
        }
        else
        {
            _aboutWindow.SetActive(false);
        }
    }

    public void CloseAboutButtonPressed()
    {
        _aboutWindow.SetActive(false);
    }
}
