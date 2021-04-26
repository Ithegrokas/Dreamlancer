using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldLadyTrigger : MonoBehaviour
{
    [SerializeField] private Animator oldLady;
    [SerializeField] private Animator boxKey;

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Key")){
            oldLady.gameObject.SetActive(true);
            boxKey.SetBool("asd",true);
        }

    }
}
