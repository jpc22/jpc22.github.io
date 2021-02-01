using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCollisionHandler : MonoBehaviour
{
    public GameObject furnitureController;
    private FurnitureController controllerScript;
    public bool settled = false;
    private int timesMoved = 0;
    private int movedMax = 150;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        controllerScript = furnitureController.GetComponent<FurnitureController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (controllerScript.debugCollisions == false && settled == false)
        {
            GameObject other = collisionInfo.gameObject;
            FurnitureCollisionHandler otherHandler = GetComponent<FurnitureCollisionHandler>();

            if (otherHandler != null && settled == false)
            {
                otherHandler.settled = true;
            }
            
            if (timesMoved < movedMax)
            {
                controllerScript.moveQueue.Enqueue(this.gameObject);
                timesMoved++;
            }
            else
            {
                Debug.Log("Furniture moved too many times");
            }
            this.gameObject.SetActive(false);

        }
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.red);
        }
    }
}
