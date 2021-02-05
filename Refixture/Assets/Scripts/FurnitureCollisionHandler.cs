using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script attached to a furniture prefab
public class FurnitureCollisionHandler : MonoBehaviour
{
    public GameObject furnitureController;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    /*
    void OnCollisionStay(Collision collisionInfo)
    {
       
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.red);
        }
    }
    */
}
