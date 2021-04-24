using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetPosition : MonoBehaviour
{
    [SerializeField] private Vector3 checkpointPosition;

    public void resetPosition()
    {
        gameObject.transform.position = checkpointPosition;
    }

    public void changeCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }
}
