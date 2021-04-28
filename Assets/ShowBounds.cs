using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBounds : MonoBehaviour
{

    void Start()
    {
        GetComponent<Renderer>().bounds.Expand(100f);
    }
    void OnDrawGizmos()
    {
        Renderer r = GetComponent<Renderer>();
        if (r)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(r.bounds.center, r.bounds.size);
        }
    }
}
