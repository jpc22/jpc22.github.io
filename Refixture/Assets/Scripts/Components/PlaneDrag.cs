using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneDrag : MonoBehaviour
{
    SettingsSO _settingsSO;
    float distance_to_screen;
    Vector3 offset;
    bool mouseDown = false;

    private void Awake()
    {
        _settingsSO = Resources.Load<SettingsSO>("Settings");
    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            mouseDown = true;
        }
    }

    private void OnMouseUp()
    {
        mouseDown = false;
    }

    void OnMouseDrag()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && mouseDown)
        {
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen)) + offset;
            float x_move = pos_move.x;
            float z_move = pos_move.z;
            if (_settingsSO.IsSnapEnabled)
            {
                x_move *= 20;
                z_move *= 20;
                x_move = Mathf.Floor(x_move) / 20;
                z_move = Mathf.Floor(z_move) / 20;
            }
            transform.position = new Vector3(x_move, transform.position.y, z_move);
        }
    }

}
