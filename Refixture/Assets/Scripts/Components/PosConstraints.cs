using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosConstraints : MonoBehaviour
{
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
        if (ThisBounds == null)
            ThisBounds = GetCombinedBoundingBoxOfChildren(transform);
        _xMin += ThisBounds.extents.x;
        _xMax -= ThisBounds.extents.x;
        _yMax -= ThisBounds.size.y;
        _zMin += ThisBounds.extents.z;
        _zMax -= ThisBounds.extents.z;
    }

    void LateUpdate()
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

    public static Bounds GetCombinedBoundingBoxOfChildren(Transform root)
    {
        if (root == null)
        {
            throw new ArgumentException("The supplied transform was null");
        }

        var colliders = root.GetComponentsInChildren<Collider>();
        if (colliders.Length == 0)
        {
            throw new ArgumentException("The supplied transform " + root?.name + " does not have any children with colliders");
        }

        Bounds totalBBox = colliders[0].bounds;
        foreach (var collider in colliders)
        {
            totalBBox.Encapsulate(collider.bounds);
        }
        return totalBBox;
    }
}
