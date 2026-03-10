using System.Collections;
using UnityEngine;

public class PlayerMeleeVisual: MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerActions actions;

    public bool isMeleeAnimationPlaying = false;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        actions = GetComponentInParent<PlayerActions>();

        DebugRegistry.Register("isMeleeAnimationPlaying", () => isMeleeAnimationPlaying);
    }

    void Start()
    {
        Hide();
    }

    public void Render()
    {
        spriteRenderer.enabled = true;
        animator.enabled = true;
        animator.Play("Melee");
    }

    public void Hide()
    {
        actions.OnMeleeAnimationEnded();
        spriteRenderer.enabled = false;
        animator.enabled = false;
    }

    public void MeleeAnimationEnded()
    {
        isMeleeAnimationPlaying = false;
        actions.OnMeleeAnimationEnded();
    }

    public void MeleeAnimationStarted()
    {
        isMeleeAnimationPlaying = true;
    }

}


