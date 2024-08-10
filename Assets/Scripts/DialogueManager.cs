using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

using Dialogue = System.Collections.Generic.IEnumerator<DialoguePart>;

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
            Dialogue dialogue = DialogueParser.Parse("test");
            StartDialogue(dialogue);
        }
    }

    private void NextDialogue()
    {
        if (currentDialogue == null || !currentDialogue.MoveNext())
        {
            dialogueBox.SetActive(false);
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
    public static Dialogue Parse(string dialogueTitle)
    {
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

            return dialogueParts.GetEnumerator();
        }
    }
}
