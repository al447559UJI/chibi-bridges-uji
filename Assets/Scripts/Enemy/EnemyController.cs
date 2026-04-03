using System.Collections;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

enum EnemyState
{
    PATROL,
    SHOOT
}
public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData data;
    [SerializeField] private LayerMask groundLayer;
    [Tooltip("Every layer the enemy will be able to see.")]
    [SerializeField] private LayerMask detectionLayers;

    private Rigidbody2D rb;
    private BoxCollider2D bodyCollider;
    private Vector2 bodySize;
    private EnemyState state;
    private EnemyStatusBubble statusBubble;
    private float currentDirectionTimer;
    private int currentHealth;
    private bool isGrounded;
    private int lastDirection;

    private int currentDirection = 1; // 1 = Right, -1 = Left.
    private float feetCollision = 0.05f;
    private bool playerDetected = false;
    private bool isShooting = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        statusBubble = GetComponentInChildren<EnemyStatusBubble>();
    }

    void Start()
    {
        bodySize = bodyCollider.bounds.size;
        currentDirectionTimer = data.directionTimer;
        currentHealth = data.health;

        state = EnemyState.PATROL;
    }

    void FixedUpdate()
    {

        SearchPlayer();
        if (playerDetected && state == EnemyState.PATROL)
        {
            state = EnemyState.SHOOT;
        }

        switch (state)
        {
            case EnemyState.PATROL:
                Move();
                CheckGrounded();
                UpdateTimer();
                CheckWall();
                if (isGrounded && IsOnPlatformEdge())
                {
                    Flip();
                }
                break;
            case EnemyState.SHOOT:
                rb.linearVelocity = Vector2.zero;
                if (!isShooting)
                {
                    StartCoroutine(Shoot(data.shootDamage));
                }
                break;
        }
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

    private void UpdateTimer()
    {
        currentDirectionTimer -= Time.fixedDeltaTime;
        if (currentDirectionTimer <= 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        currentDirection = -currentDirection;
        currentDirectionTimer = data.directionTimer;
        transform.Rotate(0f, 180f, 0f);
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            bodyCollider.bounds.center,
            bodySize,
            0f, // Angle
            Vector2.down, // Direction
            feetCollision,
            groundLayer
        );

        isGrounded = hit.collider != null;
    }

    private void CheckWall()
    {
        Bounds bounds = GetBounds();
        Vector2 rayOrigin = GetForwardCenter(bounds);
        Vector2 direction = GetForwardDirection();

        RaycastHit2D hit = Physics2D.Raycast(
            rayOrigin,
            direction,
            feetCollision,
            groundLayer
        );

        if (hit.collider != null)
        {
            Flip();
        }

    }

    private bool IsOnPlatformEdge()
    {
        Bounds bounds = GetBounds();
        Vector2 rayOrigin = GetForwardBottom(bounds);

        RaycastHit2D hit = Physics2D.Raycast(
            rayOrigin,
            Vector2.down,
            feetCollision,
            groundLayer
        );

        return hit.collider == null;
    }

    public void Damage(int damageAmount, DamageType damageType, int direction)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void SearchPlayer()
    {
        Vector2 direction = GetForwardDirection();
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            data.searchDistance,
            detectionLayers
        );

        playerDetected = hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player");
    }

    private IEnumerator Shoot(int damage)
    {
        isShooting = true;
        statusBubble.PlayWarningAnimation();
        yield return new WaitForSeconds(data.reactionCooldown);
        statusBubble.PlayEmptyAnimation();
        Debug.Log(name + " shot the player for " + damage + " damage.");
        yield return new WaitForSeconds(data.shootCooldown);
        if (!playerDetected)
        {
            state = EnemyState.PATROL;
        }
        isShooting = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 direction = (currentDirection == 1) ? Vector2.right : Vector2.left;
        Gizmos.DrawRay(transform.position, direction * data.searchDistance);
#if UNITY_EDITOR
        Handles.Label(transform.position + Vector3.up * 1.5f, state.ToString());
#endif
    }

    private Vector2 GetForwardDirection()
    {
        return (currentDirection == 1) ? Vector2.right : Vector2.left;
    }

    private Vector2 GetForwardCenter(Bounds bounds)
    {
        return (currentDirection == 1)
            ? new Vector2(bounds.max.x, bounds.center.y)
            : new Vector2(bounds.min.x, bounds.center.y);
    }

    private Vector2 GetForwardBottom(Bounds bounds)
    {
        return (currentDirection == 1)
            ? new Vector2(bounds.max.x, bounds.min.y)
            : new Vector2(bounds.min.x, bounds.min.y);
    }

    private Bounds GetBounds()
    {
        return bodyCollider.bounds;
    }

    public void MeleeAttack(IDamageable target)
    {
        target.Damage(data.meleeDamage, DamageType.MELEE, lastDirection);
    }
}
