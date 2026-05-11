using System;
using UnityEngine;

enum EnemyAltState
{
    IDLE,
    MOVE,
    HURT
}
public class EnemyAltController : MonoBehaviour, IDamageable, IHurtBoxUser, IDetectRangeUser
{
    [SerializeField] private EnemyAltData data;
    [SerializeField] private GameObject killParticleEmiter;

    private Rigidbody2D rb;
    private Animator animator;
    private EnemyAltState state;
    private EnemyStatusBubble statusBubble;
    private ItemDropBehavior dropBehavior;
    private Healthbar healthBar;
    private DetectRange detectRange;
    private int currentHealth;
    private int lastDirection;

    private int currentDirection = 1; // 1 = Right, -1 = Left.
    private bool playerDetected = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dropBehavior = GetComponent<ItemDropBehavior>();
        statusBubble = GetComponentInChildren<EnemyStatusBubble>();
        healthBar = GetComponentInChildren<Healthbar>();
        detectRange = GetComponentInChildren<DetectRange>();
    }

    void Start()
    {
        currentHealth = data.health;

        state = EnemyAltState.IDLE;
    }

    void FixedUpdate()
    {
        if (playerDetected && state == EnemyAltState.IDLE)
        {
            state = EnemyAltState.MOVE;
        }

        switch (state)
        {
            case EnemyAltState.MOVE:
                Move();
                if (TargetOnOppositeSide())
                {
                    Flip();
                }
                break;
            case EnemyAltState.IDLE:
                Stop();
                break;
            case EnemyAltState.HURT:
                Stop();
                break;
        }
    }

    public void TargetFound()
    {
        playerDetected = true;
    }

    private bool TargetOnOppositeSide()
    {
        bool result = false;

        if (detectRange.lastTargetPosition.x < transform.position.x && currentDirection == 1)
        {
            result = true;
        }
        else if (detectRange.lastTargetPosition.x > transform.position.x && currentDirection == -1)
        {
            result = true;
        }

        return result;
    }

    public void TargetLost()
    {
        playerDetected = false;
        state = EnemyAltState.IDLE;
    }

    private void Move()
    {
        float targetSpeed = currentDirection * data.maxSpeed;
        float speedDifference = targetSpeed - rb.linearVelocityX;

        // Use acceleration when input exists, deceleration when stopping
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.acceleration : data.deceleration;

        float movement = accelRate * speedDifference * Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(
            rb.linearVelocityX + movement,
            rb.linearVelocityY
        );

        lastDirection = Math.Sign(rb.linearVelocityX);
    }

    private void Stop()
    {
        rb.linearVelocity = Vector2.zero;
    }


    public void Damage(int damageAmount, DamageType damageType, int direction)
    {
        currentHealth -= damageAmount;
        state = EnemyAltState.HURT;
        animator.Play("Hurt");
        healthBar.SetSize(currentHealth, data.health);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(killParticleEmiter, transform.position, Quaternion.identity);
        dropBehavior.DropItems(4, gameObject.transform.position);
        Destroy(gameObject);
    }

    public void MeleeAttack(IDamageable target)
    {
        target.Damage(data.meleeDamage, DamageType.MELEE, lastDirection);
    }

    public void Flip()
    {
        currentDirection = -currentDirection;
        transform.Rotate(0f, 180f, 0f);
    }

}