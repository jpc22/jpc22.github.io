using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCollisionHandler : MonoBehaviour
{
    private GameObject furnitureController;
    private FurnitureController controllerScript;
    // Start is called before the first frame update
    void Start()
    {
        furnitureController = GameObject.Find("FurnitureController");
        controllerScript = furnitureController.GetComponent<FurnitureController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.red);
        }
    }
}
