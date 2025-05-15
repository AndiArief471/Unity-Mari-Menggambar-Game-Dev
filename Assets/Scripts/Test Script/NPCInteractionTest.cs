using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionTest : Interactable
{
    public TextAsset inkyJSON;

    [Header("Prompt")]
    public string textPrompt;

    public string GetPrompt()
    {
        return textPrompt;
    }

    public override void Interact()
    {
        Debug.Log("Start dialogue with: " + inkyJSON.name);
        // Call your dialogue system here
    }
}