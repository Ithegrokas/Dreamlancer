using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private TMP_Text dialogueText = null;
    [SerializeField] private GameObject pressSpace = null;
    [SerializeField] private float waitForText = 2f;
    private Queue<DialogueSegment> segments;
    private GameObject dialogue_box;
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        segments = new Queue<DialogueSegment>();
        DialogueTrigger.dialogueEvent += StartDialogueEventHandler;
        dialogue_box = nameText.transform.parent.gameObject;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void StartDialogueEventHandler(object sender, DialogueObject dialogue) 
    {
        StartDialogueSequence(dialogue);
    }

    void StartDialogueSequence(DialogueObject dialogue)
    {
        segments.Clear();

        foreach (var segment in dialogue.dialogueSegments)
        {
            segments.Enqueue(segment);
        }
        StartCoroutine(DisplayNextSentence());
    }

    IEnumerator DisplayNextSentence()
    {
        if (segments.Count == 0)
        {
            EndDialogue();
            yield break;
        }

        DialogueSegment segment = segments.Dequeue();
        nameText.text = segment.dialogueName;
        Coroutine typeSentenceRoutine = null;

        StartDialogue();

        if(typeSentenceRoutine != null)
            StopCoroutine(typeSentenceRoutine);
        
        typeSentenceRoutine =  StartCoroutine(TypeSentence(segment.dialogueText));

        yield return new WaitForSeconds(waitForText);

        pressSpace.SetActive(true);
        while(!Input.GetKeyDown(KeyCode.Space))
            yield return null;


        StartCoroutine(DisplayNextSentence());
        
    }

    void StartDialogue()
    {
        dialogue_box.SetActive(true);
        playerController.disableInput();
    }

    void EndDialogue()
    {
        pressSpace.SetActive(false);
        dialogue_box.SetActive(false);
        playerController.enableInput();
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
