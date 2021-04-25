using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private Animator enemyAnim;

    void Start() 
    {
        enemyAnim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player") && !player.isTrigger)
        {
           StartCoroutine(player.GetComponent<PlayerController>().Death());

            if (enemyAnim != null)
                enemyAnim.SetBool("PlayerHit", true);
        }
            
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            if (enemyAnim != null)
            {
                enemyAnim.SetBool("PlayerHit", false);
            }
        }
    }
}
