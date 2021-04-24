using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSystem : MonoBehaviour
{

    [SerializeField] private LayerMask unlockLayer;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Unlock"))
            col.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

}
