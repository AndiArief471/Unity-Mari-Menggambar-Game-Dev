using UnityEngine;

public class HidingManager : MonoBehaviour
{
    public static HidingManager Instance { get; private set; }

    public GameObject player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void HidePlayer()
    {
        player.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetActive(true);

        }
    }

    // public static HidingManager Instance;
    // public GameObject player;

    // private void Awake()
    // {
    //     if (Instance == null) Instance = this;
    //     else Destroy(gameObject);
    // }

    // public void HidePlayerWithSpot(HidingSpot spot)
    // {
    //     spot.anim.SetTrigger(spot.hideTrigger);
    //     StartCoroutine(HideAfterDelay(spot.anim, spot.hideTrigger));
    // }

    // public void RevealPlayerFromSpot(HidingSpot spot)
    // {
    //     spot.anim.SetTrigger(spot.comeOutTrigger);
    //     player.SetActive(true);
    // }

    // private IEnumerator HideAfterDelay(Animator anim, string clipName)
    // {
    //     float delay = GetAnimationClipLength(anim, clipName);
    //     yield return new WaitForSeconds(delay);
    //     player.SetActive(false);
    // }

    // private float GetAnimationClipLength(Animator animator, string clipName)
    // {
    //     foreach (var clip in animator.runtimeAnimatorController.animationClips)
    //     {
    //         if (clip.name == clipName)
    //             return clip.length;
    //     }
    //     return 1f;
    // }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         FindObjectOfType<HidingSpot>().ExitHiding();
    //     }
    // }
}