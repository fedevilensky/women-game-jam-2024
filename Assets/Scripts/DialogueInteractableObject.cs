public class DialogueInteractableObject : InteractableObject
{

    public override void Interact(DialogueManager.DialogueEndCallback callback = null)
    {
        var currentLevel = LevelManager.instance.currentLevel;
        var dialogueName = interactionText[currentLevel];
        if (!isInteractable)
        {
            dialogueName = "defaultDialogue";
        }

        isInteractable = false;
        var dialogue = DialogueParser.Parse(dialogueName);
        DialogueManager.instance.StartDialogue(dialogue, callback);
    }
}
