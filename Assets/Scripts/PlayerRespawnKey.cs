using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnKey : MonoBehaviour
{

    private Transform boxKey;
    private Vector3 boxKeySpawn;
    // Start is called before the first frame update
    void Start()
    {
        boxKey = GameObject.FindGameObjectWithTag("Key").transform;
        boxKeySpawn = boxKey.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            boxKey.position = boxKeySpawn;
    }
}
