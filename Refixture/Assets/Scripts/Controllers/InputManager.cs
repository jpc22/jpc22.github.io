using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO _pausedEventChannel = default;
    
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _pausedEventChannel.RaiseEvent();
            /*
            evo2D.paused = true;
            roomPopulation2D = evo2D.roomPopulation;
            DrawRooms();
            */
        }
    }
}
