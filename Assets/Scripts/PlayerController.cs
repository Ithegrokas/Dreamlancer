using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform currentCheckpointTransform;

    private int currentCheckpoint = 0;

    private bool beenHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetPlayer() 
    {
        gameObject.transform.position = currentCheckpointTransform.position;
        gameObject.transform.rotation.SetLookRotation(Vector3.zero);
    }
}
