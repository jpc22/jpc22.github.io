using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    public GameObject tableObj;
    public GameObject chairObj;
    public GameObject bedObj;
    [Range(1,4)]public int chairCount;
    void Start()
    {
        SpawnObject(tableObj, 1);
        SpawnObject(chairObj, chairCount);
        SpawnObject(bedObj, 1);
        
    }

    void Update()
    {
        
    }
    
    Vector3 RandomVector()
    {
        float posX = Random.Range(-5f, 5f);
        float posZ = Random.Range(-5f, 5f);
        return new Vector3(posX, 0, posZ);
    }

    Vector3 RandomRotation()
    {
        float rotX = 0f;
        float rotY = Random.Range(-180f, 180f);
        float rotZ = 0f;
        return new Vector3(rotX, rotY, rotZ);
    }

    void SpawnObject(GameObject obj, int objectCount)
    {
        for(int i = 0; i < objectCount; i++)
        {
            bool colliding = true;
            int retry = 0;
            while (colliding == true && retry < 100)
            {
                Vector3 xform = RandomVector();
                Vector3 rotation = RandomRotation();
                if (true || Physics.CheckBox(obj.transform.position, obj.GetComponent<Collider>().bounds.size / 2, obj.transform.rotation))
                {
                    GameObject newObj = Instantiate(obj, obj.transform.position, obj.transform.rotation) as GameObject;
                    newObj.transform.Translate(xform);
                    newObj.transform.Rotate(rotation);
                    colliding = false;
                }
                else
                {
                    colliding = true;
                    retry++;
                }
            }
        }
        
    }
}
