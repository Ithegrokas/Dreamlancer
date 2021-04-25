using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private DialogueObject dialogue;
    public static event EventHandler<DialogueObject> dialogueEvent;
    void OnTriggerEnter2D(Collider2D other) 
    {
        dialogueEvent?.Invoke(this, dialogue);
    }
}