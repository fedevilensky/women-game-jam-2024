using UnityEngine;


public abstract class InteractableObject : MonoBehaviour
{
  public string[] interactionText;
  public bool isInteractable
  {
    get
    {
      return _isInteractable;
    }
    set
    {
      _isInteractable = value;
      if (value)
      {
        GetComponent<SpriteRenderer>().color = Color.white;
      }
      else
      {
        GetComponent<SpriteRenderer>().color = Color.gray;
      }
    }
  }

  private bool _isInteractable = false;
  public abstract void Interact(DialogueManager.DialogueEndCallback callback = null);
}
