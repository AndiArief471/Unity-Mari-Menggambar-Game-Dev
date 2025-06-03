using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject customButton;
    public GameObject optionPanel;
    public static bool isDialoguePlaying { get; private set; } = false;
    public static bool isChoiceOptionAppear { get; private set; } = false;

    static Story story;
    static Choice choiceSelected;
    public CameraManager cameraManager;
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
        if (story != null && Input.GetKeyDown(KeyCode.Space) && !isChoiceOptionAppear)
        // if (story != null && Input.GetMouseButton(0) && !isChoiceOptionAppear)
        {
            // First, check if any NPC is still typing
            foreach (NPCInteraction npc in FindObjectsOfType<NPCInteraction>())
            {
                if (npc.FinishTyping()) return;
            }
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
        HideSpeechBubble();

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

    private void HideSpeechBubble()
    {
        // Hide all speech bubbles
        foreach (NPCInteraction npc in FindObjectsOfType<NPCInteraction>())
        {
            npc.HideSpeech();
        }
    }

    IEnumerator ShowChoices()
    {
        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
        }

        optionPanel.SetActive(true);
        isChoiceOptionAppear = true;
        HideSpeechBubble();
        cameraManager.SwitchCamera(cameraManager.choicePanelCam);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    public static void SetDecision(object element)
    {
        choiceSelected = element as Choice;
        story.ChooseChoiceIndex(choiceSelected.index);
        if (choiceSelected.index == 0) { // To overcame Empty line appearing when player choose the first option
            story.Continue();
        }
    }

    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        isChoiceOptionAppear = false;
        cameraManager.SwitchCamera(cameraManager.mainCam);

        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }

        choiceSelected = null;
        
            // NEW: Call Continue again if story can continue immediately
        if (story.canContinue)
        {
            AdvanceDialogue(); // show the next line
        }
        else if (story.currentChoices.Count > 0)
        {
            StartCoroutine(ShowChoices()); // show next choices if there are any
        }
        else
        {
            FinishDialogue(); // no more story
        }
    }

    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");

        HideSpeechBubble();

        story = null;
        isDialoguePlaying = false;
    }

}