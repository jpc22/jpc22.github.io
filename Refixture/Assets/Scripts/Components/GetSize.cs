using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        //Debug.Log(gameObject.name + " Size M: " + collider.size.magnitude);
        Debug.Log(gameObject.name + " Bounds M: " + collider.bounds.size.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
