using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RefixUtilities;
using UnityEngine.UI;

public class ObjectMeasurer : MonoBehaviour
{
    public float l;
    public float w;
    public float h;
    void Start()
    {
        Bounds bounds = RefixUtility.GetCombinedBoundingBoxOfChildren(transform);
        l = bounds.size.x;
        w = bounds.size.z;
        h = bounds.size.y;
    }

}
