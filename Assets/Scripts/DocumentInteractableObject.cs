public class DocumentInteractableObject : InteractableObject
{

  public override void Interact(DialogueManager.DialogueEndCallback callback = null)
  {
    var documentName = interactionText;
    if (!isInteractable)
    {
      documentName = "defaultDocument";
    }

    DialogueManager.instance.StartDocument(documentName, callback);
  }

}
