using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    private Transform _foundation;
    private Transform _playerCamera;

    Material tempMaterial;
    Color opaqueColor;
    Color transparentColor;
    float _transparentAlpha = 0.0f;
    float _opaqueAlpha = 1.0f;
    float _checkInterval = 0.5f;

    public Transform Foundation { get => _foundation; set => _foundation = value; }
    public Transform PlayerCamera { get => _playerCamera; set => _playerCamera = value; }
    public float TransparentAlpha
    {
        get => _transparentAlpha; set
        {
            _transparentAlpha = value;
            transparentColor.a = _transparentAlpha;
        }
    }

    public float OpaqueAlpha
    {
        get => _opaqueAlpha; set
        {
            _opaqueAlpha = value;
            opaqueColor.a = _opaqueAlpha;
        }
    }

    void Start()
    {
        Renderer rend = GetComponentInChildren<Renderer>();
        tempMaterial = new Material(rend.material);
        opaqueColor = tempMaterial.color;
        _opaqueAlpha = opaqueColor.a;
        transparentColor = tempMaterial.color;
        transparentColor.a = _transparentAlpha;
        rend.material = tempMaterial;
        InvokeRepeating("CheckDistance", 0f, _checkInterval);
    }

    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, PlayerCamera.position) < Vector3.Distance(Foundation.position, PlayerCamera.position))
        {
            if(tempMaterial.color.a != _transparentAlpha)
                StartCoroutine(LerpAlpha(opaqueColor, transparentColor, _checkInterval - 0.1f));
        }
        else
        {
            if(tempMaterial.color.a != _opaqueAlpha)
                StartCoroutine(LerpAlpha(transparentColor, opaqueColor, _checkInterval - 0.1f));
        }
    }

    IEnumerator LerpAlpha(Color startVal, Color endVal, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            tempMaterial.color = Color.Lerp(startVal, endVal, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        tempMaterial.color = endVal;
    }
}
