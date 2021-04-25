using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueText", menuName = "CAS_JAM/DialogueText", order = 0)]
public class DialogueText : ScriptableObject 
{
    [SerializeField] private List<string> dialogueText = new List<string>();
}
