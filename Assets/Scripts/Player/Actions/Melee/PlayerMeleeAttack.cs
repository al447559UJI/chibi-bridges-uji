using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerActions actions;
    private BoxCollider2D hitbox;

    public bool isMeleeAnimationPlaying = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        actions = GetComponentInParent<PlayerActions>();

        DebugRegistry.Register("isMeleeAnimationPlaying", () => isMeleeAnimationPlaying);
    }

    void Start()
    {
        Hide();
    }

    public void InitializeHitbox(int damage, LayerMask damageableLayer)
    {
        // In case hitboxes break later
        //Vector2 size = Vector2.Scale(hitbox.size, hitbox.transform.lossyScale);

        Collider2D[] hits = Physics2D.OverlapBoxAll(
            hitbox.transform.position,
            hitbox.size,
            0f,
            damageableLayer);

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable = hit.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
        }

    }

    void OnDrawGizmos()
    {
        if (hitbox != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(
                hitbox.transform.position,
                hitbox.size
            );
        }
    }

    public void Render()
    {
        spriteRenderer.enabled = true;
        animator.enabled = true;
        animator.Play("Melee");
    }

    public void Hide()
    {
        actions.OnMeleeAnimationEnded(); // Should remove this
        spriteRenderer.enabled = false;
        animator.enabled = false;
    }

    public void MeleeAnimationEnded() // Called by Animator
    {
        isMeleeAnimationPlaying = false;
        actions.OnMeleeAnimationEnded();
    }

    public void MeleeAnimationStarted()
    {
        isMeleeAnimationPlaying = true;
    }

}