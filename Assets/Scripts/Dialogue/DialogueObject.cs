using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "CAS_JAM/DialogueObject", order = 0)]
public class DialogueObject : ScriptableObject
{

    [Header("Dialogue")]
    public List<DialogueSegment> dialogueSegments = new List<DialogueSegment>();

}

[System.Serializable]
public struct DialogueSegment
{
    public string dialogueName;
    [TextArea(3, 7)] public string dialogueText;

}

