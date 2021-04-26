using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private DialogueObject dialogue = null;
    public static event EventHandler<DialogueObject> dialogueEvent;
    void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.CompareTag("Player") && !col.isTrigger) 
        {
            dialogueEvent?.Invoke(this, dialogue);
            gameObject.SetActive(false);
        }
            
    }
}