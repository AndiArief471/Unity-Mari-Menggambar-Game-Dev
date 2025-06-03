using UnityEngine;

public class HidingSpot : Interactable
{
    [Header("Prompt")]
    public string textPrompt = "Hide";

    public Animator hideAnim;
    public Animator comingOutAnim;

    public override string GetPrompt()
    {
        return textPrompt;
    }
    public override void Interact()
    {
        HidingManager.Instance.HidePlayer();
    }

    // public Animator anim;  // The spot’s animator
    // public string hideTrigger = "Hide";
    // public string comeOutTrigger = "ComeOut";

    // public override string GetPrompt() => "Hide";

    // public override void Interact()
    // {
    //     HidingManager.Instance.HidePlayerWithSpot(this);
    // }

    // public void ExitHiding()
    // {
    //     HidingManager.Instance.RevealPlayerFromSpot(this);
    // }
}