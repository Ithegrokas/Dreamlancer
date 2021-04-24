using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D player) 
    {
        player.GetComponent<ResetPosition>().resetPosition();    
    }
}
