using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Unlock"))
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }
    }
}
