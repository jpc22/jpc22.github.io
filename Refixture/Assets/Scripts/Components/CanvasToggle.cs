using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    Canvas canvas;
    bool hideUI = false;
    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            hideUI = !hideUI;
            canvas.enabled = !hideUI;
        }
    }
}
