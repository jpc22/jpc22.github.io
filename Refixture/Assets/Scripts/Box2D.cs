using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box2D : MonoBehaviour
{
    public bool colliding = false;
    public Room2D controller;
    public bool collidingWall = false;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        colliding = true;
        if (collision.gameObject.CompareTag("Wall"))
        {
            collidingWall = true;
        }
    }

    private void OnCollisionExit2D()
    {
        colliding = false;
        collidingWall = false;
    }
}
