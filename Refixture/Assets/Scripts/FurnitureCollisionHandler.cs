﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script attached to a furniture prefab
public class FurnitureCollisionHandler : MonoBehaviour
{
    public FurnitureController furnitureController;
    GameObject marker;
    private void Awake()
    {
        marker = Resources.Load<GameObject>("Marker");   
    }
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Sofa") || gameObject.CompareTag("Desk"))
        {
            GameObject o = Instantiate(marker, gameObject.transform, false);
            o.transform.localPosition += Vector3.right + new Vector3(0, 0.5f);
        }
        //elif (gameObject.CompareTag(""))
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
