using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private InteractableObject[] interactableObjects;

    private int currentLevel = 0;

    [SerializeField]
    private Sprite[] levelSprites;

    public static LevelManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        interactableObjects = FindObjectsOfType<InteractableObject>();

        SetLevel(0);
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
        foreach (var interactableObject in interactableObjects)
        {
            interactableObject.isInteractable = interactableObject.activeLevel == level;
        }

        if (level == 0)
        {
            PlayIntro();
        }

        if (level == 1)
        {
            GetComponent<GameManager>().ResetLoop();
            var dialogue = DialogueParser.Parse("start1");
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }

    void PlayIntro()
    {
        GetComponent<BackgroundUpdater>().SetBackground(0);
        var dialogue = DialogueParser.Parse("intro");
        DialogueManager.instance.StartDialogue(dialogue);
    }


    public void CheckNextLevel()
    {
        if (interactableObjects.All(interactableObject => !interactableObject.isInteractable))
        {
            SetLevel(currentLevel + 1);
        }
    }
}
