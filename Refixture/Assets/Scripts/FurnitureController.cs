using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    public List<GameObject> fixtures = new List<GameObject>();
    [Range(1,4)]public int chairCount;
    public bool debugCollisions;
    public Queue<GameObject> spawnQueue = new Queue<GameObject>();
    public Queue<GameObject> moveQueue = new Queue<GameObject>();
    public float spawnRate = 0f;
    private IEnumerator spawnRoutine;
    private IEnumerator moveRoutine;
    void Start()
    {
        spawnQueue.Enqueue(fixtures[0]);
        for(int i = 0; i < chairCount; i++)
        {
            spawnQueue.Enqueue(fixtures[1]);
        }
        spawnQueue.Enqueue(fixtures[2]);
        spawnQueue.Enqueue(fixtures[3]);
        spawnQueue.Enqueue(fixtures[4]);
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
                MoveObject(obj);
                obj.GetComponent<FurnitureCollisionHandler>().settled = false;
                obj.SetActive(true);
            }

        }

    }

    Vector3 RandomVector()
    {
        float posX = Random.Range(-3.75f, 3.75f);
        float posZ = Random.Range(-3.75f, 3.75f);
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
        GameObject newObj = Instantiate(obj, obj.transform.position, obj.transform.rotation);
        MoveObject(newObj);
    }

    public void MoveObject(GameObject obj)
    {
        Vector3 xform = RandomVector();
        Vector3 rotation = RandomRotation();
        obj.transform.position = xform;
        obj.transform.Rotate(rotation);
    }
}
