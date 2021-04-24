using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D player) 
    {
        if(player.gameObject.CompareTag("Player"))
            player.GetComponent<ResetPosition>().resetPosition();    
    }
}
