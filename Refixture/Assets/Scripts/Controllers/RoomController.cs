using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script attached to a room prefab
public class RoomController : MonoBehaviour
{
    public GameObject[] walls;
    public List<bool> wallTearaways = new List<bool>();

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tearAwayRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator tearAwayRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < walls.Length; i++)
            {
                if (wallTearaways[i])
                {
                    walls[i].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }
                else
                {
                    walls[i].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
            }
        }
    }
}
