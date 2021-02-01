using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>(); // list holds each furniture object assigned to the room
    public bool debugCollisions; // if enabled, stops rearranging furniture that is colliding
    public Queue<GameObject> spawnQueue = new Queue<GameObject>();
    public Queue<GameObject> moveQueue = new Queue<GameObject>();
    public float spawnRate = 0f; // delay between spawning and moving furniture, set 0 to delay 1 frame
    public float xRange = 3.75f;
    public float zRange = 3.75f;
    void Start()
    {
        StartCoroutine(SpawnRoutine(spawnRate));
        StartCoroutine(MoveRoutine(spawnRate));
    }

    void Update()
    {
        
    }

    IEnumerator SpawnRoutine(float waitTime)
    {
        while (true)
        {
            if (spawnRate > 0)
            {
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield return null;
            }
            if (spawnQueue.Count > 0)
            {
                GameObject obj = spawnQueue.Dequeue();
                SpawnObject(obj);
            }
            
        }
        
    }

    IEnumerator MoveRoutine(float waitTime)
    {
        while (true)
        {
            if (spawnRate > 0)
            {
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield return null;
            }
            if (moveQueue.Count > 0)
            {
                GameObject obj = moveQueue.Dequeue();
                MoveObject(obj, RandomVector(), RandomRotation());
                obj.GetComponent<FurnitureCollisionHandler>().settled = false;
                obj.SetActive(true);
            }

        }

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

    public void SpawnObject(GameObject obj)
    {
        GameObject newObj = Instantiate(obj, gameObject.transform, false);
        newObj.GetComponent<FurnitureCollisionHandler>().furnitureController = gameObject;
        objectList.Add(newObj);
        MoveObject(newObj, RandomVector(), RandomRotation());
    }

    public void MoveObject(GameObject obj, Vector3 newPos, Vector3 rotation)
    {
        obj.transform.position = newPos + obj.transform.parent.position;
        obj.transform.Rotate(rotation);
    }
}
