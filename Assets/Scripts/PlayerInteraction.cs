using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public InteractableObject currentInteractableObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (currentInteractableObject != null)
            {
                string interactionText;
                if (currentInteractableObject.isInteractable)
                {
                    interactionText = currentInteractableObject.interactionText;
                }
                else
                {
                    interactionText = "defaultDialogue";
                }
                print("interacting with " + interactionText);
                var dialogue = DialogueParser.Parse(interactionText);
                DialogueManager.instance.StartDialogue(dialogue);
                currentInteractableObject.isInteractable = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableObject interactableObject = collision.GetComponent<InteractableObject>();
        if (interactableObject != null)
        {
            currentInteractableObject = interactableObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractableObject interactableObject = collision.GetComponent<InteractableObject>();
        if (interactableObject == currentInteractableObject)
        {
            currentInteractableObject = null;
        }
    }
}
