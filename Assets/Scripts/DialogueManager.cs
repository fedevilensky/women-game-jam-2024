using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

using Dialogue = System.Collections.Generic.IEnumerator<DialoguePart>;


class Pair<T1, T2>
{
    public T1 First;
    public T2 Second;

    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }
}

public class DialogueManager : MonoBehaviour
{
    private GameObject player;
    public static DialogueManager instance;

    public GameObject dialogueBox;

    public TextMeshProUGUI textMeshPro;

    private Dialogue currentDialogue;

    void Awake()
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
            Dialogue dialogue = DialogueParser.Parse("test");
            StartDialogue(dialogue);
        }
    }

    private void NextDialogue()
    {
        if (currentDialogue == null || !currentDialogue.MoveNext())
        {
            dialogueBox.SetActive(false);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;

            return;
        }
        var part = currentDialogue.Current;

        textMeshPro.text = part.text;
    }


    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogueBox.activeSelf)
        {
            return;
        }

        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        currentDialogue = dialogue;
        dialogueBox.SetActive(true);
        currentDialogue.MoveNext();
        var part = currentDialogue.Current;
        textMeshPro.text = part.text;
    }
}

public struct DialoguePart
{
    public string text;
    public string emotion;
}

public class DialogueParser
{
    private static Pair<string, IEnumerable<DialoguePart>>[] dialogueBuffer =
        new Pair<string, IEnumerable<DialoguePart>>[10];
    private static int dialogueBufferIndex = 0;
    private static int dialogueBufferLength = 0;
    public static Dialogue Parse(string dialogueTitle)
    {

        for (int i = 0; i < dialogueBufferLength; i++)
        {
            var index = (dialogueBufferIndex + i) % dialogueBuffer.Length;
            if (dialogueBuffer[index].First == dialogueTitle)
            {
                return dialogueBuffer[index].Second.GetEnumerator();
            }
        }

        using (TextReader r = new StreamReader("Assets/Dialogues/" + dialogueTitle + ".txt"))
        {
            var dialogueParts = new List<DialoguePart>();
            string line;
            bool isNextEmotion = true;

            string emotion = "", text = "";

            while ((line = r.ReadLine()) != null)
            {
                if (isNextEmotion)
                {
                    if (line == "") continue;

                    emotion = line;
                    isNextEmotion = false;
                }
                else
                {
                    if (line != "")
                    {
                        text = line;
                    }

                    isNextEmotion = true;
                    dialogueParts.Add(new DialoguePart { emotion = emotion, text = text != "" ? text : "..." });
                    text = "";
                    emotion = "";
                }
            }

            if (emotion != "")
            {
                dialogueParts.Add(new DialoguePart { emotion = emotion, text = text != "" ? text : "..." });
            }

            if (dialogueBufferLength == dialogueBuffer.Length)
            {
                dialogueBufferIndex = (dialogueBufferIndex + 1) % dialogueBuffer.Length;
            }
            else
            {
                dialogueBufferLength++;
                dialogueBufferIndex = dialogueBufferLength - 1;
            }

            dialogueBuffer[dialogueBufferIndex] = new Pair<string, IEnumerable<DialoguePart>>(
                dialogueTitle, dialogueParts);

            return dialogueParts.GetEnumerator();
        }
    }
}
