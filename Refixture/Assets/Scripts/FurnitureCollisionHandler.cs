using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCollisionHandler : MonoBehaviour
{
    private GameObject furnitureController;
    private FurnitureController controllerScript;
    public bool settled = false;
    private void Awake()
    {
        furnitureController = GameObject.Find("FurnitureController");
        controllerScript = furnitureController.GetComponent<FurnitureController>();
    }
    // Start is called before the first frame update
    void Start()
    {

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
            
            controllerScript.moveQueue.Enqueue(this.gameObject);
            this.gameObject.SetActive(false);

        }
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.red);
        }
    }
}
