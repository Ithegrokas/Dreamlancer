using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnKey : MonoBehaviour
{

    [SerializeField] private Transform boxKey = null;
    private ResetPosition playerCheckpoint;

    void Start() 
    {
        playerCheckpoint = GetComponent<ResetPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            boxKey.position = playerCheckpoint.getCheckpoint();
    }
}
