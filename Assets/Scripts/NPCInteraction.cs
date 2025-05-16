using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : Interactable
{
    [Header("NPC Identity")]
    public string npcName;

    [Header("Dialogue (Optional)")]
    public TextAsset inkyJSON;

    [Header("Speech Bubble")]
    public GameObject speechBubble;
    public TextMeshProUGUI speechText;

    [Header("Prompt")]
    public string textPrompt;

    public string GetPrompt()
    {
        return textPrompt;
    }

    public override void Interact()
    {
        Debug.Log("Start dialogue with: " + inkyJSON.name);
        DialogueManager.Instance.StartDialogue(inkyJSON);
    }

    public void ShowSpeech(string currentSentence)
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentSentence));
        }
        else
        {
            Debug.Log("Bubble Speech is Null");
        }
    }

    public void HideSpeech()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
            speechText.text = "";
        }
    }

    IEnumerator TypeSentence(string sentence, float delayTime = 0.025f)
    {
        speechText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(delayTime);
        }
        yield return null;
    }
}