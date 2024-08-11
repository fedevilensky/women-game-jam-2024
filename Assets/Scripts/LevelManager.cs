using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private InteractableObject[] interactableObjects;

    public int currentLevel = 0;

    [SerializeField]
    private Sprite[] levelSprites;

    public static LevelManager instance;

    public TimeManager timeManager;

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


        Debug.Log("Level " + level);
        if (level == 0)
        {
            GameObject.FindObjectOfType<ExperimentTableSpriteHandler>().SetExperimentTableSprite(false);
            PlayIntro();
        }

        if (level == 2)
        {
            GameObject.FindObjectOfType<ExperimentTableSpriteHandler>().SetExperimentTableSprite(false);
            timeManager.StartTicking();
            GetComponent<GameManager>().ResetLoop();
            var dialogue = DialogueParser.Parse("start1");
            DialogueManager.instance.StartDialogue(dialogue, () => timeManager.PauseTicking());
        }

        foreach (var interactableObject in interactableObjects)
        {
            interactableObject.isInteractable = interactableObject.interactionText[level] != "";
        }
    }

    void PlayIntro()
    {
        GameObject.FindWithTag("Background").GetComponent<BackgroundUpdater>().SetBackground(0);
        var dialogue = DialogueParser.Parse("intro");
        DialogueManager.instance.StartDialogue(dialogue, () => timeManager.PauseTicking());
    }


    public void CheckNextLevel()
    {
        if (interactableObjects.All(interactableObject => !interactableObject.isInteractable))
        {
            SetLevel(currentLevel + 1);
        }
    }
}
