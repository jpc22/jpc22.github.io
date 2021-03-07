using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehavior script that contains the information of the 2d and 3d fixture.
/// </summary>
public class Fixture : MonoBehaviour
{
    [SerializeField] private FurnishingSO _fixtureSO;
    private GameObject _prefab2dInstance;
    private GameObject _prefab3dInstance;
    private Transform _transform2d;
    private Transform _transform3d;

    public FurnishingSO FixtureSO
    {
        get => _fixtureSO; set
        {
            _fixtureSO = value;
            gameObject.name = value.Name;
        }
    }
    public GameObject Prefab2dInstance
    {
        get => _prefab2dInstance; set
        {
            _prefab2dInstance = value;
            Transform2d = value.transform;
        }
    }
    public GameObject Prefab3dInstance
    {
        get => _prefab3dInstance; set
        {
            _prefab3dInstance = value;
            Transform3d = value.transform;
        }
    }
    public Transform Transform2d { get => _transform2d; set => _transform2d = value; }
    public Transform Transform3d { get => _transform3d; set => _transform3d = value; }

    public void Spawn3dFixture()
    {
        if (FixtureSO != null)
        {
            if (FixtureSO.Prefab3d != null)
            {
                Prefab3dInstance = Instantiate(FixtureSO.Prefab3d, this.transform, false);
            }
            else Debug.LogError("No 3d prefab assigned to SO " + FixtureSO.Name);
        }
        else Debug.LogError("No fixtureSO assigned to fixture.");
    }

    public void Spawn2dFixture()
    {
        if (FixtureSO != null)
        {
            if (FixtureSO.Prefab2d != null)
            {
                Prefab2dInstance = Instantiate(FixtureSO.Prefab2d, this.transform, false);
            }
            else Debug.LogError("No 2d prefab assigned to SO " + FixtureSO.Name);
        }
        else Debug.LogError("No fixtureSO assigned to fixture.");
    }
    /// <summary>
    /// Spawns the 3d prefab by scanning and translating the 2d prefab's transform. 
    /// </summary>
    public void Spawn3dFixtureFrom2d()
    {
        if (FixtureSO != null)
        {
            if (FixtureSO.Prefab3d != null)
            {
                Prefab3dInstance = Instantiate(FixtureSO.Prefab3d, this.transform, false);
            }
            else Debug.LogError("No 3d prefab assigned to SO " + FixtureSO.Name);
        }
        else Debug.LogError("No fixtureSO assigned to fixture.");
    }
    /// <summary>
    /// Sets the position of the fixture in both 2d or 3d
    /// </summary>
    /// <param name="newPos"></param>
    public void SetLocalPosition(Vector3 newPos)
    {
        if (Transform2d != null)
            Transform2d.localPosition = newPos;
        if (Transform3d != null)
            Transform3d.localPosition = newPos;
    }
    /// <summary>
    /// Sets the rotation of the fixture in both 2d or 3d
    /// </summary>
    /// <param name="newRot"></param>
    public void SetLocalRotation(Quaternion newRot)
    {
        if (Transform2d != null)
            Transform2d.localRotation = newRot;
        if (Transform3d != null)
            Transform3d.localRotation = newRot;
    }

    public void Toggle2dFixture()
    {
        if (Prefab2dInstance != null)
        {
            bool active = Prefab2dInstance.activeSelf;
            Prefab2dInstance.SetActive(!active);
        }
    }

    public void Toggle3dFixture()
    {
        if (Prefab3dInstance != null)
        {
            bool active = Prefab3dInstance.activeSelf;
            Prefab3dInstance.SetActive(!active);
        }
    }
}
