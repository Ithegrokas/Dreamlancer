using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 checkpointPosition;

    void Start()
    {
        checkpointPosition = gameObject.transform.position;
    }
    
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
            player.GetComponent<ResetPosition>().changeCheckpoint(checkpointPosition);
    }
}
