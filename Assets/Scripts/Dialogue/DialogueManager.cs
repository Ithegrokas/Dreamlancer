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
    [SerializeField] private RectTransform dialogueBox = null;
    private Queue<DialogueSegment> segments;
    private Vector2 dialogueBoxDown = new Vector2(0f, 156f);
    private Vector2 dialogueBoxUp = new Vector2(0f, 612f);
    private PlayerController playerController;
    private bool textIsWritten = false;
    private Camera main;

    // Start is called before the first frame update
    void Start()
    {
        segments = new Queue<DialogueSegment>();
        DialogueTrigger.dialogueEvent += StartDialogueEventHandler;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        main = Camera.main;
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

        if (typeSentenceRoutine != null)
            StopCoroutine(typeSentenceRoutine);

        typeSentenceRoutine = StartCoroutine(TypeSentence(segment.dialogueText));

        yield return WaitForText();

        pressSpace.SetActive(true);
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;

        StartCoroutine(DisplayNextSentence());

    }

    void StartDialogue()
    {
        bool playerIsUp = playerController.transform.position.y > main.transform.position.y;

        if (playerIsUp)
            dialogueBox.anchoredPosition = dialogueBoxDown;
        else
            dialogueBox.anchoredPosition = dialogueBoxUp;

        dialogueBox.gameObject.SetActive(true);
        playerController.disableInput();
    }

    void EndDialogue()
    {
        pressSpace.SetActive(false);
        dialogueBox.gameObject.SetActive(false);
        playerController.enableInput();
    }

    IEnumerator WaitForText()
    {
        while (!textIsWritten)
            yield return null;
    }

    IEnumerator TypeSentence(string sentence)
    {
        textIsWritten = false;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        textIsWritten = true;
    }
}
