using System;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueBox;

    public TextMeshProUGUI textMeshPro;

    private Dialogue currentDialogue;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            dialogueBox.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            var dialogue = new Dialogue(new string[] { "Hello, I am a dialogue box!", "I am a second dialogue box!" });
            StartDialogue(dialogue);
        }
    }

    private void NextDialogue()
    {
        currentDialogue.currentDialogueIndex++;
        if (currentDialogue.currentDialogueIndex >= currentDialogue.dialogues.Length)
        {
            dialogueBox.SetActive(false);
            return;
        }

        textMeshPro.text = currentDialogue.dialogues[currentDialogue.currentDialogueIndex];
    }


    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogueBox.activeSelf)
        {
            return;
        }

        currentDialogue = dialogue;
        dialogueBox.SetActive(true);
        textMeshPro.text = dialogue.dialogues[dialogue.currentDialogueIndex];
    }
}

public struct Dialogue
{
    public Dialogue(string[] dialogues)
    {
        this.dialogues = dialogues;
        currentDialogueIndex = 0;
    }
    public string[] dialogues;

    public int currentDialogueIndex;
}
