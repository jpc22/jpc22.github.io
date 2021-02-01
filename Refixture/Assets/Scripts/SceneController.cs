using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour
{
    public GameObject roomPrefab;
    public int populationCount = 9;
    public List<GameObject> roomPrefabs = new List<GameObject>();
    public List<GameObject> furnitureList = new List<GameObject>();
    private void Awake()
    {
        float xOffset = 0;
        float zOffset = 0;
        for (int i = 0; i < Math.Sqrt(populationCount); i++)
        {
            for (int j = 0; j < Math.Sqrt(populationCount); j++)
            {
                roomPrefabs.Add(Instantiate(roomPrefab, roomPrefab.transform.position + new Vector3(xOffset, 0, zOffset), roomPrefab.transform.rotation));
                xOffset += 7.5f;
            }
            xOffset = 0f;
            zOffset += 7.5f;
        }
        
        
        for (int i = 0; i < roomPrefabs.Count; i++)
        {
            for (int j = 0; j < furnitureList.Count; j++)
            {
                //roomPrefabs[i].GetComponent<FurnitureController>().debugCollisions = true;
                roomPrefabs[i].GetComponent<FurnitureController>().spawnQueue.Enqueue(furnitureList[j]);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
