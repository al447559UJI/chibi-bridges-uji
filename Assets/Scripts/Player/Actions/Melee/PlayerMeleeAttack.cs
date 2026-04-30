using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerActions actions;
    private PlayerScrap playerScrap;
    private BoxCollider2D hitbox;
    private DamageType damageType;


    void Awake()
    {
        animator = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        actions = GetComponentInParent<PlayerActions>();
        playerScrap = GetComponentInParent<PlayerScrap>();
    }

    void Start()
    {
        Hide();
        damageType = DamageType.MELEE;
    }

    public void InitializeHitbox(int damage, LayerMask damageableLayer)
    {
        // In case hitboxes break later, try this:
        // Vector2 size = Vector2.Scale(hitbox.size, hitbox.transform.lossyScale);

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
                damageable.Damage(damage, damageType, 0);
            }

            PoleHitbox pole = hit.GetComponent<PoleHitbox>();
            if (pole != null)
            {
                Debug.Log("Giving the player " + pole.destroyScrapAmount + " scrap.");
                playerScrap.Give(pole.destroyScrapAmount);
                pole.DestroyPole();
            }

            Trashcan trashcan = hit.GetComponent<Trashcan>();
            if (trashcan != null)
            {
                trashcan.HandleAttack();
            }
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
        spriteRenderer.enabled = false;
        animator.enabled = false;
    }

    // Animation Event: Called at the end of the melee animation clip.
    public void OnMeleeAnimationEnded()
    {
        actions.OnMeleeAnimationEnded();
    }

    // Animation Event: Called at the start of the melee animation clip.
    public void OnMeleeAnimationStarted()
    {

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
}