using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private TMP_Text dialogueText = null;
    [SerializeField] private Animator animator = null;
    private Queue<DialogueSegment> segments;
    
    // Start is called before the first frame update
    void Start()
    {
        segments = new Queue<DialogueSegment>();
        DialogueTrigger.dialogueEvent += StartDialogueEventHandler;
    }

    void StartDialogueEventHandler(object sender, DialogueObject dialogue) 
    {
        StartDialogue(dialogue);
    }

    void StartDialogue(DialogueObject dialogue)
    {
        animator.SetBool("IsOpen", true);
        segments.Clear();

        foreach (var segment in dialogue.dialogueSegments)
        {
            segments.Enqueue(segment);
        }
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (segments.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueSegment segment = segments.Dequeue();
        nameText.text = segment.dialogueName;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(segment.dialogueText));
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
