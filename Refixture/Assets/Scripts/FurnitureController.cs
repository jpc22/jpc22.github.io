using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script attached to a room prefab
public class FurnitureController : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();
    public float xRange = 3.75f;
    public float zRange = 3.75f;

    void Start()
    {

    }

    void Update()
    {

    }

    Vector3 RandomVector()
    {
        float posX = Random.Range(-xRange, xRange);
        float posZ = Random.Range(-zRange, zRange);
        return new Vector3(posX, 0, posZ);
    }

    Vector3 RandomRotation()
    {
        float rotX = 0f;
        float rotY = Random.Range(-180f, 180f);
        float rotZ = 0f;
        return new Vector3(rotX, rotY, rotZ);
    }

    public void SpawnObject(GameObject obj, Vector3 pos, Vector3 rotation)
    {
        GameObject newObj = Instantiate(obj, gameObject.transform, false);
        newObj.GetComponent<FurnitureCollisionHandler>().furnitureController = gameObject.GetComponent<FurnitureController>();
        objectList.Add(newObj);
        obj.transform.localPosition = pos;
        obj.transform.localEulerAngles = rotation;
    }

}
