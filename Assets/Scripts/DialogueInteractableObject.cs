public class DialogueInteractableObject : InteractableObject
{

    public override void Interact(DialogueManager.DialogueEndCallback callback = null)
    {
        var dialogueName = interactionText;
        if (!isInteractable)
        {
            dialogueName = "defaultDialogue";
        }

        var dialogue = DialogueParser.Parse(dialogueName);
        DialogueManager.instance.StartDialogue(dialogue, callback);
    }
}
