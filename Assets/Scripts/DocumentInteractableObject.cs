public class DocumentInteractableObject : InteractableObject
{

  public override void Interact(DialogueManager.DialogueEndCallback callback = null)
  {
    var currentLevel = LevelManager.instance.currentLevel;
    var documentName = interactionText[currentLevel];
    print(isInteractable);
    if (!isInteractable)
    {
      var dialogue = DialogueParser.Parse("defaultDialogue");
      DialogueManager.instance.StartDialogue(dialogue, callback);
      return;
    }

    isInteractable = false;
    DialogueManager.instance.StartDocument(documentName, callback);
  }

}
