using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public TimeManager timeManager;

    private Pair<string, Sprite>[] dialogueImages = new Pair<string, Sprite>[10];
    private int dialogueImagesIndex = 0;
    private int dialogueImagesLength = 0;
    public static DialogueManager instance;

    public GameObject dialogueBox;

    public TextMeshProUGUI textMeshPro;

    [SerializeField]
    private int imageHeight = 128;
    private Dialogue currentDialogue;

    public delegate void DialogueEndCallback();

    private DialogueEndCallback callback;

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
            timeManager?.ResumeTicking();
            LevelManager.instance.CheckNextLevel();
            callback?.Invoke();
            callback = null;
            return;
        }
        var part = currentDialogue.Current;
        var dialogueImage = GetImage(part.emotion);

        GameObject.FindWithTag("DialogueImage").GetComponent<Image>().sprite = dialogueImage;
        textMeshPro.text = part.text;
    }


    private Sprite GetImage(string name)
    {
        for (int i = 0; i < dialogueImagesLength; i++)
        {
            if (dialogueImages[i].First == name)
            {
                return dialogueImages[i].Second;
            }
        }

        Texture2D texture = LoadTexture("Assets/Dialogues/Images/" + name + ".png");
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0, 0),
            texture.height * 1f / imageHeight
        );

        if (dialogueImagesLength == dialogueImages.Length)
        {
            dialogueImagesIndex = (dialogueImagesIndex + 1) % dialogueImages.Length;
        }
        else
        {
            dialogueImagesLength++;
            dialogueImagesIndex = dialogueImagesLength - 1;
        }


        dialogueImages[dialogueImagesIndex] = new Pair<string, Sprite>(name, sprite);

        return sprite;
    }

    private Texture2D LoadTexture(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2); // size is irrelevant, it will be overwritten
        texture.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        return texture;
    }




    public void StartDialogue(Dialogue dialogue, DialogueEndCallback callback = null)
    {
        if (dialogueBox.activeSelf)
        {
            return;
        }

        this.callback = callback;
        timeManager?.PauseTicking();
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        currentDialogue = dialogue;
        dialogueBox.SetActive(true);
        NextDialogue();
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
            if (dialogueBuffer[i].First == dialogueTitle)
            {
                return dialogueBuffer[i].Second.GetEnumerator();
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
