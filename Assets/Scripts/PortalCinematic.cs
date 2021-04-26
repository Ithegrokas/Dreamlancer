using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCinematic : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player"))
        {
            PlayerController playerController = col.GetComponent<PlayerController>();
            playerController.disableInput();

            col.GetComponent<Animator>().Play("Cinematic");

            StartCoroutine(col.GetComponent<PlayerCinematic>().playCinematic());
        }
    }
}
