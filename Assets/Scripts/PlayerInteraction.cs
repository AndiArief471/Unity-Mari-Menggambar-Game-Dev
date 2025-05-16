using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactionKey;
    public TextMeshProUGUI promptText;

    private Interactable currentInteractable;
    private string currentPrompt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentInteractable = interactable;

            // Try to get a text prompt, if the object has one
            if (interactable is NPCInteraction npc)
            {
                currentPrompt = npc.GetPrompt();
            }
            else
            {
                currentPrompt = "Press E to interact"; // fallback
            }
            promptText.enabled = true;
            promptText.text = currentPrompt;
            Debug.Log("Test Colliding");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>() == currentInteractable)
        {
            currentInteractable = null;
            promptText.enabled = false;
        }
    }
    private void Start()
    {
        promptText.enabled = false;
    }
    private void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact();
            promptText.enabled = false;
        }
    }
    // public TextMeshProUGUI promptText;
    // string interactionText = "";

    // // Update is called once per frame
    // void Update()
    // {
    //     promptText.text = interactionText;
    // }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Interactable interactable = collision.GetComponent<Interactable>();
    //     interactionText = interactable.promptText;
    //     Debug.Log("object just collided");
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     interactionText = "";
    // }
}
