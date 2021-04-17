using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    private Transform _foundation;
    private Transform _playerCamera;

    Renderer[] renderers;
    List<Material> materialList;
    List<Color> colorList;
    Material transparentMaterial;
   
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
        }
    }

    public float OpaqueAlpha
    {
        get => _opaqueAlpha; set
        {
            _opaqueAlpha = value;
        }
    }

    void Start()
    {
        materialList = new List<Material>();
        colorList = new List<Color>();
        renderers = GetComponentsInChildren<Renderer>();
        transparentMaterial = Resources.Load<Material>("Transparent");

        foreach (Renderer r in renderers)
        {
            var materials = r.materials;
            for(int i = 0; i < materials.Length; i++)
            {
                colorList.Add(materials[i].color);
                materials[i] = new Material(transparentMaterial);
                materials[i].color = colorList[i];
            }
            r.materials = materials;
            materialList.AddRange(materials);
        }
        
        InvokeRepeating("CheckDistance", 0f, _checkInterval);
    }

    void SetAlpha(Material mat, float value)
    {
        Color c = mat.color;
        c.a = value;
        mat.color = c;
    }

    void CheckDistance()
    {
        SetAlpha(transparentMaterial, TransparentAlpha);
        if (Vector3.Distance(transform.position, PlayerCamera.position) < Vector3.Distance(Foundation.position, PlayerCamera.position))
        {
            foreach (Material mat in materialList)
            {
                if (materialList[0].color.a == _transparentAlpha)
                    break;
                StartCoroutine(LerpAlpha(mat, transparentMaterial.color, _checkInterval - 0.1f));
            }
        }
        else
        {
            for (int i = 0; i < materialList.Count; i++)
            {
                if (materialList[0].color.a == _opaqueAlpha)
                    break;
                StartCoroutine(LerpAlpha(materialList[i], colorList[i], _checkInterval - 0.1f));
            }
        }
    }


    IEnumerator LerpAlpha(Material mat, Color endVal, float duration)
    {
        float time = 0;

        Color startVal = mat.color;
        while (time < duration)
        {
            mat.color = Color.Lerp(startVal, endVal, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        mat.color = endVal;
    }
}
