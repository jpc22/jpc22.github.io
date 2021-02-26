using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//root script of the room scene
public class SceneController : MonoBehaviour
{
    public GameObject roomEvoPrefab;
    public GameObject roomPrefab;
    public Vector2 roomWidth = new Vector2(7.5f, 7.5f);
    private RoomEvo evo2D;
    public List<GameObject> roomPopulation = new List<GameObject>(); // candidate of population
    public List<GameObject> roomPopulation2D = new List<GameObject>();
    public List<GameObject> furniturePrefabs = new List<GameObject>(); // List of furniture prefabs
    public TextMeshProUGUI avgFitText;

    private void Awake()
    {
        GameObject scene2D = Instantiate(roomEvoPrefab);
        scene2D.transform.localPosition = new Vector3(0, 60f);
        evo2D = scene2D.GetComponent<RoomEvo>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            evo2D.paused = true;
            roomPopulation2D = evo2D.roomPopulation;
            DrawRooms();
        }
        avgFitText.text = "Avg. Fit: " + evo2D.GetComponent<RoomEvo>().avg_fit;
    }

    void DrawRooms()
    {
        float xOffset = 0;
        float zOffset = 0;
        for (int i = 0; i < Mathf.Sqrt(roomPopulation2D.Count); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(roomPopulation2D.Count); j++)
            {
                GameObject room = Instantiate(roomPrefab, gameObject.transform, false);
                roomPopulation.Add(room);
                room.transform.localPosition += new Vector3(xOffset, 0, zOffset);
                xOffset += roomWidth.x;
            }
            xOffset = 0f;
            zOffset += roomWidth.y;
        }

        for(int i = 0; i < roomPopulation2D.Count; i++)
        {
            GameObject room2D = roomPopulation2D[i];
            FurnitureController room3D = roomPopulation[i].GetComponent<FurnitureController>();
            List<GameObject> furnitureBoxes = room2D.GetComponent<Room2D>().Boxes;
            for (int j = 0; j < furnitureBoxes.Count; j++)
            {
                GameObject box = furnitureBoxes[j];
                GameObject furn = furniturePrefabs[j];
                Vector3 pos = box.transform.localPosition;
                Vector3 rot = box.transform.localEulerAngles;
                GameObject newObj = Instantiate(furn, room3D.gameObject.transform, false);
                newObj.GetComponent<FurnitureCollisionHandler>().furnitureController = room3D.gameObject.GetComponent<FurnitureController>();
                newObj.transform.localPosition = new Vector3(pos.x, 0, pos.y);
                newObj.transform.localEulerAngles = new Vector3(0, -rot.z, 0);
                room3D.objectList.Add(newObj);
            }
        }
    }
}
