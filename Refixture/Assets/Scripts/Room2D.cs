using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2D : MonoBehaviour
{
    public List<GameObject> Boxes;
    float xLength = 7.5f;
    float yHeight = 7.5f;
    float wallThickness = 0.1f;
    public float fitnessVal;
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
        Box2D box = obj.GetComponent<Box2D>();
        Vector2 v = obj.GetComponent<SpriteRenderer>().bounds.size;
        if (box.isWallFixture)
        {
            if (pos == Vector2.zero)
            {
                setRandomWallPosition(obj);
            }
            else
            {
                obj.transform.localPosition = pos;
                obj.transform.localRotation = rot;
            }
            
            /*
            if (Mathf.Abs(obj.transform.localPosition.x) > Mathf.Abs(obj.transform.localPosition.y))
            {
                obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            }
            else if (Mathf.Abs(obj.transform.localPosition.x) < Mathf.Abs(obj.transform.localPosition.y))
            {
                obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            }
            */
            obj.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
        else
        {
            if (pos == Vector2.zero)
            {
                pos = randomPosition(v);
            }
            if (rot.z == 0)
            {
                rot.eulerAngles = randomRotation();
            }
            obj.transform.localPosition = pos;
            obj.transform.localRotation = rot;
        }
        
    }

    Vector3 randomRotation()
    {
        return new Vector3(0, 0, Random.Range(-180f, 180f));
    }

    Vector2 randomPosition(Vector2 size)
    {
        float xBounds = (xLength - size.x - wallThickness) / 2;
        float yBounds = (yHeight - size.y - wallThickness) / 2;
        return new Vector2(Random.Range(-xBounds, xBounds), Random.Range(-yBounds, yBounds));
    }

    void setRandomWallPosition(GameObject obj)
    {
        Vector2 v = obj.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 cardinality;
        int randInt = Random.Range(0, 4);
        if (randInt == 0) 
        { 
            cardinality = Vector2.right;
            obj.transform.localEulerAngles = new Vector3(0, 0, 180);
            setCardinalPosition(obj, cardinality, v);
        }
        else if (randInt == 1) 
        { 
            cardinality = Vector2.down;
            obj.transform.localEulerAngles = new Vector3(0, 0, 90);
            setCardinalPosition(obj, cardinality, v);
        }
        else if (randInt == 2) 
        { 
            cardinality = Vector2.left;
            obj.transform.localEulerAngles = new Vector3(0, 0, 0);
            setCardinalPosition(obj, cardinality, v);
        }
        else 
        { 
            cardinality = Vector2.up;
            obj.transform.localEulerAngles = new Vector3(0, 0, -90);
            setCardinalPosition(obj, cardinality, v);
        }
    }

    void setCardinalPosition(GameObject obj, Vector2 cardinality, Vector2 v)
    {
        if (cardinality == Vector2.up || cardinality == Vector2.down)
        {
            obj.transform.localPosition = new Vector2(cardinality.x * ((xLength - v.y - wallThickness) / 2), cardinality.y * ((yHeight - v.x - wallThickness) / 2));
        }
        else
        {
            obj.transform.localPosition = new Vector2(cardinality.x * ((xLength - v.x - wallThickness) / 2), cardinality.y * ((yHeight - v.y - wallThickness) / 2));
        }
        

        if (obj.transform.localPosition.x == 0)
        {
            obj.transform.localPosition = new Vector2(Random.Range(-xLength / 2 + v.x, xLength / 2 - v.x), obj.transform.localPosition.y);
        }
        else
        {
            obj.transform.localPosition = new Vector2(obj.transform.localPosition.x, Random.Range(-yHeight / 2 + v.y, yHeight / 2 - v.y));
        }
    }

    public void updateFitness()
    {
        fitnessVal = 1;
        foreach (GameObject box in Boxes)
        {
            Box2D boxController = box.GetComponent<Box2D>();
            if (boxController.isCollidingWall)
            {
                fitnessVal += 10;
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
