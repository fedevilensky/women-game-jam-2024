using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public InteractableObject currentInteractableObject;

    private float lastInteraction = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastInteraction > 0.3f && Input.GetKeyUp(KeyCode.Space))
        {
            if (currentInteractableObject != null)
            {
                currentInteractableObject.Interact(() => lastInteraction = Time.time);
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
