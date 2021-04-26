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
        if (player.CompareTag("Player"))
        {
           StartCoroutine(player.GetComponent<PlayerController>().Death());

            if (!gameObject.CompareTag("Flame"))
                enemyAnim.SetBool("PlayerHit", true);
        }
            
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            if (!gameObject.CompareTag("Flame"))
            {
                enemyAnim.SetBool("PlayerHit", false);
            }
        }
    }
}
