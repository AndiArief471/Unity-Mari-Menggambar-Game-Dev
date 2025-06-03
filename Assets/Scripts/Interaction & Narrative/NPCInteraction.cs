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
    public float typingSpeed { get; private set; } = 0.025f;
    public bool IsTyping { get; private set; } = false;
    private Coroutine typingCoroutine;
    private string fullSentence;

    public override string GetPrompt()
    {
        return textPrompt;
    }

    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(inkyJSON);
    }

    public void ShowSpeech(string currentSentence)
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(true);
            StopAllCoroutines();
            fullSentence = currentSentence;
            typingCoroutine = StartCoroutine(TypeSentence(fullSentence));
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

    IEnumerator TypeSentence(string sentence)
    {
        IsTyping = true;
        speechText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        IsTyping = false;
        yield return null;
    }

    public bool FinishTyping()
    {
        if (IsTyping)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);
            speechText.text = fullSentence;
            IsTyping = false;
            return true;
        }
        return false;
    }
}