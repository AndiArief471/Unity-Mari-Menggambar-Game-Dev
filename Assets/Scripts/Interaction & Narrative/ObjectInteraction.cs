using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : Interactable
{
    [Header("Prompt")]
    public string textPrompt;

    public override string GetPrompt()
    {
        return textPrompt;
    }

    public override void Interact()
    {
        Debug.Log("Test");
        // Call your dialogue system here
    }
}