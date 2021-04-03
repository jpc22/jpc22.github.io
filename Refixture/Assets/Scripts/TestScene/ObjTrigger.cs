using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTrigger : MonoBehaviour
{
    public Rigidbody thisRb;
    public MeshRenderer meshRend;

    public float red;
    // Start is called before the first frame update
    void Start()
    {
        red = 0f;
        meshRend.material.color = new Color(red,255f,255f);
        var colliders = GetComponentsInChildren<Collider>();

        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Trigger"))
            {
                collider.enabled = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        red += 20f;
        meshRend.material.color = new Color(red, 20f, 20f);
    }

    private void OnTriggerExit(Collider other)
    {
        red -= 20f;
        meshRend.material.color = new Color(red, 20f, 20f);
    }
}
