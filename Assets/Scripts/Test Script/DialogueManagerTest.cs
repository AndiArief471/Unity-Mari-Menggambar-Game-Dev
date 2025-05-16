

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using Ink.Runtime;

// public class DialogueManagerTest : MonoBehaviour
// {
//     public TextAsset inkFile;
//     public GameObject textBox;
//     public GameObject customButton;
//     public GameObject optionPanel;

//     static Story story;
//     TextMeshProUGUI message;
//     static Choice choiceSelected;
//     // Start is called before the first frame update
//     void Start()
//     {
//         story = new Story(inkFile.text);
//         message = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
//         choiceSelected = null;
//         AdvanceDialogue();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(Input.GetKeyDown(KeyCode.Space))
//         {
//             //Is there more to the story?
//             if(story.canContinue)
//             {
//                 AdvanceDialogue();

//                 //Are there any choices?
//                 if (story.currentChoices.Count != 0)
//                 {
//                     StartCoroutine(ShowChoices());
//                 }
//             }
//             else
//             {
//                 FinishDialogue();
//             }
//         }
//     }

//     private void FinishDialogue()
//     {
//         Debug.Log("End of Dialogue!");
//     }

//     // Advance through the story 
//     void AdvanceDialogue()
//     {
//         string currentSentence = story.Continue();
//         StopAllCoroutines();
//         StartCoroutine(TypeSentence(currentSentence));
//     }

//     IEnumerator TypeSentence(string sentence, float delayTime = 0.025f)
//     {
//         message.text = "";
//         Debug.Log(message);
//         foreach(char letter in sentence.ToCharArray())
//         {
//             message.text += letter;
//             yield return new WaitForSeconds(delayTime);
//         }
//         yield return null;
//     }

//     IEnumerator ShowChoices()
//     {
//         Debug.Log("There are choices need to be made here!");
//         List<Choice> _choices = story.currentChoices;
//         Debug.Log(_choices.Count);

//         for (int i = 0; i < _choices.Count; i++)
//         {
//             GameObject temp = Instantiate(customButton, optionPanel.transform);
//             temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _choices[i].text;
//             temp.AddComponent<Selectable>();
//             temp.GetComponent<Selectable>().element = _choices[i];
//             temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
//         }

//         optionPanel.SetActive(true);

//         yield return new WaitUntil(() => { return choiceSelected != null; });

//         AdvanceFromDecision();
//     }

//     public static void SetDecision(object element)
//     {
//         choiceSelected = (Choice)element;
//         story.ChooseChoiceIndex(choiceSelected.index);
//     }

//      void AdvanceFromDecision()
//     {
//         optionPanel.SetActive(false);
//         for (int i = 0; i < optionPanel.transform.childCount; i++)
//         {
//             Destroy(optionPanel.transform.GetChild(i).gameObject);
//         }
//         choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
//         AdvanceDialogue();
//     }

// }

