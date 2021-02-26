using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2D : MonoBehaviour
{
    public bool colliding = false;
    public Room2D controller;
    public bool isCollidingWall = false;
    public bool isWallFixture;
    public List<Vector3> functionFaces;

    void Start()
    {
        
    }
    void Update()
    {
        // Debug.DrawRay(gameObject.transform.localPosition, gameObject.transform.right, Color.green);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        colliding = true;
        if (collision.gameObject.CompareTag("Wall"))
        {
            isCollidingWall = true;
        }
    }

    private void OnCollisionExit2D()
    {
        colliding = false;
        isCollidingWall = false;
    }
}
