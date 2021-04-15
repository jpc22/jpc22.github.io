﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevateDrag : MonoBehaviour
{
    SettingsSO _settingsSO;
    float distance_to_screen;
    Vector3 offset;

    private void Awake()
    {
        _settingsSO = Resources.Load<SettingsSO>("Settings");
    }
    private void OnMouseDown()
    {
        distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
    }
    void OnMouseDrag()
    {
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen)) + offset;
        float y_move = pos_move.y;
        if (_settingsSO.IsSnapEnabled)
        {
            y_move *= 20;
            y_move = Mathf.Floor(y_move) / 20;
        }
        transform.position = new Vector3(transform.position.x, y_move, transform.position.z);
    }
}