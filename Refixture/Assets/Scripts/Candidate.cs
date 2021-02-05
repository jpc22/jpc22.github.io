using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script attached to a room prefab
public class Candidate : MonoBehaviour
{
    public FurnitureController furnitureController;
    public List<GameObject> geneRepresentation;
    private void Awake()
    {
        furnitureController = gameObject.GetComponent<FurnitureController>();
        geneRepresentation = furnitureController.objectList;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

}
