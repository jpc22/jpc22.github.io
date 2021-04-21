using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateDrag : MonoBehaviour
{
    private SettingsSO _settingsSO;
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;
    private void Awake()
    {
        _settingsSO = Resources.Load<SettingsSO>("Settings");
    }
    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && _isRotating)
        {
            if (_settingsSO.IsSnapEnabled)
            {
                _sensitivity = 5f;
            }
            else
            {
                _sensitivity = 0.4f;
            }
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            //if (_settingsSO.IsSnapEnabled)
           // {
            //    _rotation.y = NearestDegrees(-(_mouseOffset.x * 4));
           // }
           // else
            //{
                _rotation.y = -(_mouseOffset.x) * _sensitivity;
            //}
            // rotate
            transform.Rotate(_rotation);

            if(_settingsSO.IsSnapEnabled)
            {
                Vector3 rot = transform.localEulerAngles;
                rot.y = NearestDegrees(rot.y);
                transform.localEulerAngles = rot;
            }

            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }

    float NearestDegrees(float value)
    {
        float increment = 15f;
        float rem = value % increment;
        if (rem < increment / 2)
        {
            value = value - rem;
        }
        else
        {
            value = value + increment - rem;
        }
        return value;
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // rotating flag
            _isRotating = true;

            // store mouse
            _mouseReference = Input.mousePosition;
        }
            
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

}
