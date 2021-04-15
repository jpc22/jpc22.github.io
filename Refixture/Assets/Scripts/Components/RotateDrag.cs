using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            if (_settingsSO.IsSnapEnabled)
            {
                _rotation.y = NearestDegrees(-(_mouseOffset.x * 4));
            }
            else
            {
                _rotation.y = -(_mouseOffset.x) * _sensitivity;
            }
            // rotate
            transform.Rotate(_rotation);

            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }

    float NearestDegrees(float value)
    {
        float rem = value % 15;
        value = value - rem;
        return value;
    }

    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

}
