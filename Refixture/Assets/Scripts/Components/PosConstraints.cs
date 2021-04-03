using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RefixUtilities;
public class PosConstraints : MonoBehaviour
{
    [SerializeField]
    private float _xMin, _xMax, _yMin, _yMax, _zMin, _zMax;
    private float _xMinI, _xMaxI, _yMinI, _yMaxI, _zMinI, _zMaxI;
    private bool _set = false;
    private Bounds _thisBounds;

    public Bounds ThisBounds { get => _thisBounds; set => _thisBounds = value; }
    public float XMin { get => _xMin; }
    public float XMax { get => _xMax; }
    public float YMin { get => _yMin; }
    public float YMax { get => _yMax; }
    public float ZMin { get => _zMin; }
    public float ZMax { get => _zMax; }

    private void Start()
    {
        if (!_set)
        {
            SetLocalConstraints();
        }
        else
        {
            SetConstraintsOnBounds();
        }
    }

    public void SetLocalConstraints(float xMin = Mathf.NegativeInfinity, float xMax = Mathf.Infinity, float yMin=Mathf.NegativeInfinity,
        float yMax=Mathf.Infinity, float zMin=Mathf.NegativeInfinity, float zMax=Mathf.Infinity)
    {
        _xMinI = xMin;
        _xMaxI = xMax;
        _yMinI = yMin;
        _yMaxI = yMax;
        _zMinI = zMin;
        _zMaxI = zMax;

        _xMin = xMin;
        _xMax = xMax;
        _yMin = yMin;
        _yMax = yMax;
        _zMin = zMin;
        _zMax = zMax;

        _set = true;
    }
    public void SetConstraintsOnBounds()
    {
        ThisBounds = RefixUtility.GetCombinedBoundingBoxOfChildren(gameObject.transform);
        float xDiff = ThisBounds.extents.x;
        float zDiff = ThisBounds.extents.z;
        if (xDiff > zDiff)
        {
            _xMin = _xMinI + zDiff;
            _zMin = _zMinI + zDiff;
            _xMax = _xMaxI - zDiff;
            _zMax = _zMaxI - zDiff;
        }
        else
        {
            _xMin = _xMinI + xDiff;
            _zMin = _zMinI + xDiff;
            _xMax = _xMaxI - xDiff;
            _zMax = _zMaxI - xDiff;
        }
        _yMax -= ThisBounds.size.y;
    }

    void LateUpdate()
    {
        PositionReset();

        RotationReset();
    }

    private void PositionReset()
    {
        Vector3 pos = transform.localPosition;

        if (pos.x < _xMin)
            pos.x = _xMin;
        else if (pos.x > _xMax)
            pos.x = _xMax;

        if (pos.y < _yMin)
            pos.y = _yMin;
        else if (pos.y > _yMax)
            pos.y = _yMax;

        if (pos.z < _zMin)
            pos.z = _zMin;
        else if (pos.z > _zMax)
            pos.z = _zMax;

        transform.localPosition = pos;
    }

    private void RotationReset()
    {
        Vector3 rot = gameObject.transform.localEulerAngles;
        rot.x = 0;
        rot.z = 0;
        gameObject.transform.localEulerAngles = rot;
    }
}
