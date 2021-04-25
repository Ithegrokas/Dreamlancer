using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue = null;
    public static event EventHandler<Dialogue> dialogueEvent;

    public void TriggerDialog()
    {
        dialogueEvent?.Invoke(this, dialogue);
    }
}