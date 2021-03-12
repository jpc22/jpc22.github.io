using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCameraController : MonoBehaviour
{
    public GameObject content;
    private GameObject _model;

    public GameObject Model
    {
        get => _model; set
        {
            _model = value;
            InitializeModel();
        }
    }

    private void InitializeModel()
    {
        UpdateCameraView();
    }

    public void UpdateCameraView()
    {
        BoxCollider collider = _model.GetComponent<BoxCollider>();
        float magnitude = collider.bounds.size.magnitude;
        content.transform.localPosition = new Vector3(content.transform.localPosition.x, content.transform.localPosition.y, magnitude + 1f);
        transform.LookAt(content.transform.position + collider.center);
    }


    // Update is called once per frame
    void Update()
    {
        if (_model == null && content.transform.childCount > 0)
        {
            Model = content.transform.GetChild(0).gameObject;
        }
    }
}
