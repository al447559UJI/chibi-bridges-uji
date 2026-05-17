using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;
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

    public void InitializeHitbox(int damage, LayerMask damageableLayer, int direction)
    {
        Vector2 hitboxOrigin = hitbox.transform.position;

        Collider2D[] hits = Physics2D.OverlapBoxAll(
            new Vector2(hitboxOrigin.x + hitbox.offset.x * direction, hitboxOrigin.y + hitbox.offset.y),
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
                playerScrap.Give(pole.destroyScrapAmount);
                pole.DestroyPole();
            }

            Trashcan trashcan = hit.GetComponent<Trashcan>();
            if (trashcan != null)
            {
                trashcan.HandleAttack();
            }
        }

        if (hits.Length > 0) SoundManager.instance.PlaySound(hitSound, transform.position);
    }

    public void Render(bool isGrounded)
    {
        spriteRenderer.enabled = true;
        animator.enabled = true;
        if (isGrounded)
        {
            animator.Play("Melee");
        }
        else
        {
            animator.Play("MeleeAir");
        }
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
}