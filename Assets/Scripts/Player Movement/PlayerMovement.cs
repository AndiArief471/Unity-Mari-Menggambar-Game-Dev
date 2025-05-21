using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    // public TextMeshProUGUI promptText;
    float speedX;
    Rigidbody2D rb;
    // string interactionText = "";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.isDialoguePlaying)
        {
            rb.velocity = Vector2.zero; // Freeze movement
            return; // Don't process movement or interaction
        }

        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rb.velocity = new Vector2(speedX, 0);
        // promptText.text = interactionText;

    }

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
