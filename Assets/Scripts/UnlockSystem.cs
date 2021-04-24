using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSystem : MonoBehaviour
{

    [SerializeField] private LayerMask unlockLayer;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Unlock"))
            col.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            
    }

}
