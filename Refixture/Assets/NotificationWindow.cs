using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationWindow : MonoBehaviour
{
    [SerializeField] GameObject _window;
    
    public void OkayPressed()
    {
        _window.SetActive(false);
    }
}
