using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject customButton;
    public GameObject optionPanel;
    public static bool isDialoguePlaying { get; private set; } = false;

    static Story story;
    static Choice choiceSelected;
    List<string> tags;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(TextAsset inkJSON)
    {
        story = new Story(inkJSON.text);
        isDialoguePlaying = true;
        AdvanceDialogue();
    }

    void Update()
    {
        if (story != null && Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceDialogue();
        }
    }

    void AdvanceDialogue()
    {
        if (story == null) return;

        if (story.canContinue)
        {
            string currentSentence = story.Continue();
            Debug.Log(currentSentence);

            ParseTags(currentSentence);

            if (story.currentChoices.Count != 0)
            {
                StartCoroutine(ShowChoices());
            }
        }
        else
        {
            FinishDialogue();
        }
    }

    void ParseTags(string currentSentence)
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];
            Debug.Log(prefix);
            Debug.Log(param);
            switch (prefix.ToLower())
            {
                case "speaker":
                    ShowSpeechBubble(currentSentence, param);
                    break;
            }
        }
    }

    private void ShowSpeechBubble(string currentSentence, string speaker)
    {
        // Hide all speech bubbles
        foreach (NPCInteraction npc in FindObjectsOfType<NPCInteraction>())
        {
            npc.HideSpeech();
        }

        // Show the correct NPC’s speech bubble
        foreach (NPCInteraction npc in FindObjectsOfType<NPCInteraction>())
        {
            if (npc.npcName == speaker)
            {
                npc.ShowSpeech(currentSentence);
                return;
            }
        }
    }

    IEnumerator ShowChoices()
    {
        Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = story.currentChoices;
        Debug.Log(_choices.Count);

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        AdvanceDialogue();
    }

    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");

        foreach (NPCInteraction npc in FindObjectsOfType<NPCInteraction>())
        {
            npc.HideSpeech();
        }

        story = null;
        isDialoguePlaying = false;
    }

}