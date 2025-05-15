using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    string interactionText = "";

    // Update is called once per frame
    void Update()
    {
        promptText.text = interactionText;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();
        interactionText = interactable.promptText;
        Debug.Log("object just collided");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactionText = "";
    }
}
