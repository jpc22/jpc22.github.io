using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvo : MonoBehaviour
{
    public GameObject roomPrefab;
    public List<GameObject> boxPrefabs = new List<GameObject>();
    public List<GameObject> roomPopulation;
    public List<GameObject> furnPrefabs;
    public float avg_fit;

    int populationSize;
    float mutationRate;
    float crossoverRate;
    Vector2 roomWidth;

    [Header("Listening on Channels:")]
    [SerializeField] VoidEventChannelSO _pausedEventChannel = default;
    public bool paused = false;

    private void Awake()
    {
        _pausedEventChannel.OnEventRaised += () => paused = !paused;
    }
    void Start()
    {
        furnPrefabs = new List<GameObject>();
        for (int i = 0; i < boxPrefabs.Count; i++)
        {
            furnPrefabs.Add(boxPrefabs[i]);
        }
        populationSize = 64;
        mutationRate = 1.0f / (populationSize * furnPrefabs.Count);
        crossoverRate = 0.95f;
        roomWidth = new Vector2(7.5f, 7.5f);
        avg_fit = 0;
        CreatePopulation();
        updateFitness();
        normalizeFitness();
        InvokeRepeating("CreateNewGeneration", 1.0f, 0.5f);
    }

    public void CreatePopulation()
    {
        roomPopulation = new List<GameObject>();
        GenerateRooms();

        foreach (GameObject room in roomPopulation)
        {
            foreach (GameObject prefab in furnPrefabs)
            {
                room.GetComponent<Room2D>().spawnBox(prefab, Vector2.zero, new Quaternion(0, 0, 0, 0));
            }
        }
    }

    void GenerateRooms()
    {
        float xOffset = 0;
        float yOffset = 0;
        for (int i = 0; i < Mathf.Sqrt(populationSize); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(populationSize); j++)
            {
                GameObject room = Instantiate(roomPrefab, gameObject.transform, false);
                roomPopulation.Add(room);
                room.transform.localPosition += new Vector3(xOffset, yOffset);
                xOffset += roomWidth.x;
            }
            xOffset = 0f;
            yOffset += roomWidth.y;
        }
    }

    public void CreateNewGeneration()
    {
        if (!paused)
        {
            List<GameObject> matingPool = GetMatingPool();
            foreach (GameObject room in roomPopulation)
            {
                //room.SetActive(false);
                Destroy(room);
            }
            roomPopulation = new List<GameObject>();
            GenerateRooms();
            List<GameObject> childGeneration = Crossover(matingPool);
            for (int i = 0; i < roomPopulation.Count; i++)
            {
                Room2D child = childGeneration[i].GetComponent<Room2D>();
                Room2D room = roomPopulation[i].GetComponent<Room2D>();
                for (int j = 0; j < furnPrefabs.Count; j++)
                {
                    room.spawnBox(furnPrefabs[j], child.Boxes[j].transform.localPosition, child.Boxes[j].transform.localRotation);
                    if (mutationRate > Random.Range(0, 1f))
                    {
                        room.mutate(j);
                        Debug.Log("mutated");
                    }
                    else if (mutationRate > Random.Range(0, 1f))
                    {
                        room.mutateRotation(j);
                        Debug.Log("mutated rotation");
                    }
                }
            }
            updateFitness();
            normalizeFitness();
        }
    }

    public void updateFitness()
    {
        foreach (GameObject room in roomPopulation)
        {
            room.GetComponent<Room2D>().updateFitness();
        }
    }

    public void normalizeFitness()
    {
        float fitnessSum = 0;
        foreach (GameObject room in roomPopulation)
        {
            fitnessSum += room.GetComponent<Room2D>().fitnessVal;
        }
        foreach (GameObject room in roomPopulation)
        {
            room.GetComponent<Room2D>().fitnessNormalized = room.GetComponent<Room2D>().fitnessVal / fitnessSum;
        }
        avg_fit = fitnessSum / roomPopulation.Count;
    }

    public List<GameObject> GetMatingPool()
    {
        List<GameObject> matingPool = new List<GameObject>();
        
        while (matingPool.Count <= populationSize)
        {
            GameObject candidate = roomPopulation[UnityEngine.Random.Range(0, roomPopulation.Count)];
            if (candidate.GetComponent<Room2D>().fitnessNormalized >= UnityEngine.Random.Range(0, 1.0f))
            {
                matingPool.Add(candidate);
            }
        }
        return matingPool;
    }

    public List<GameObject> Crossover(List<GameObject> matingPool)
    {
        for(int i = 0; i < matingPool.Count - 1; i += 2)
        {
            GameObject p1 = matingPool[i];
            GameObject p2 = matingPool[i + 1];
            if (crossoverRate > UnityEngine.Random.Range(0f, 1f))
            {
                List<GameObject> p1Boxes = p1.GetComponent<Room2D>().Boxes;
                List<GameObject> p2Boxes = p2.GetComponent<Room2D>().Boxes;
                for (int j = 0; j < p1Boxes.Count; j++)
                {
                    if (crossoverRate > UnityEngine.Random.Range(0f, 1f))
                    {
                        (Vector3, Quaternion) temp = (p1Boxes[j].transform.localPosition, p1Boxes[j].transform.localRotation);
                        p1Boxes[j].transform.localPosition = p2Boxes[j].transform.localPosition;
                        p1Boxes[j].transform.localRotation = p2Boxes[j].transform.localRotation;
                        p2Boxes[j].transform.localPosition = temp.Item1;
                        p2Boxes[j].transform.localRotation = temp.Item2;
                    }
                }
            }
        }
        return matingPool;
    }
}
