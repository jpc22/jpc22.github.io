using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2D : MonoBehaviour
{
    public List<GameObject> Boxes;
    float xLength = 7.5f;
    float yHeight = 7.5f;
    public float fitnessVal = 1;
    public float fitnessNormalized;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void spawnBox(GameObject obj, Vector2 pos, Quaternion rot)
    {
        GameObject box = Instantiate(obj, gameObject.transform, false);
        box.GetComponent<Box2D>().controller = gameObject.GetComponent<Room2D>();
        Boxes.Add(box);
        positionBox(box, pos, rot);
    }

    void positionBox(GameObject obj, Vector2 pos, Quaternion rot)
    {
        if (pos == Vector2.zero)
        {
            Vector2 v = obj.GetComponent<BoxCollider2D>().size;
            float size = Mathf.Sqrt(v.x * v.x + v.y * v.y);
            pos = randomPosition(size);
        }
        if (rot.z == 0)
        {
            rot.eulerAngles = randomRotation();
        }
        obj.transform.localPosition = pos;
        obj.transform.localRotation = rot;
        
    }

    Vector3 randomRotation()
    {
        return new Vector3(0, 0, Random.Range(-180f, 180f));
    }

    Vector2 randomPosition(float objSize)
    {
        return new Vector2(Random.Range(-xLength/2 + objSize / 4, xLength/2 - objSize / 4), Random.Range(-yHeight/2 + objSize / 4, yHeight/2 - objSize / 4));
    }

    public void updateFitness()
    {
        foreach (GameObject box in Boxes)
        {
            Box2D boxController = box.GetComponent<Box2D>();
            if (boxController.collidingWall)
            {
                fitnessVal += 1;
            }
        }
    }

    public void mutate(int index)
    {
        positionBox(Boxes[index], Vector2.zero, new Quaternion(0, 0, 0, 0));
    }

    public void mutateRotation(int index)
    {
        positionBox(Boxes[index], Boxes[index].transform.localPosition, new Quaternion(0, 0, 0, 0));
    }
}
