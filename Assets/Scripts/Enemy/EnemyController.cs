using System.Collections;
using UnityEngine;
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
    [SerializeField] private LayerMask playerLayer;
    private Rigidbody2D rb;
    private BoxCollider2D bodyCollider;
    private Vector2 bodySize;
    private EnemyState state;
    private float currentDirectionTimer;
    private int currentHealth;
    private bool isGrounded;
    private float lastShootTime;

    private int currentDirection = 1; // 1 = Right, -1 = Left.
    private float feetCollision = 0.05f;
    private bool playerDetected = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
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
                if (isGrounded && IsOnPlatformEdge())
                {
                    Flip();
                }
                break;
            case EnemyState.SHOOT:
                rb.linearVelocity = Vector2.zero;
                if (Time.time - lastShootTime > (data.reactionCooldown + data.shootCooldown))
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

    private bool IsOnPlatformEdge()
    {
        Bounds bounds = bodyCollider.bounds;

        Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        Vector2 rayOrigin = (currentDirection == 1) ? bottomRight : bottomLeft;

        RaycastHit2D hit = Physics2D.Raycast(
            rayOrigin,
            Vector2.down,
            feetCollision,
            groundLayer
        );

        return hit.collider == null;
    }

    public void Damage(int damageAmount)
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
        Vector2 direction = (currentDirection == 1) ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            data.searchDistance,
            playerLayer
        );

        if (hit.collider != null)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }

    private IEnumerator Shoot(int damage)
    {
        lastShootTime = Time.time;
        Debug.Log("(!) " + name + " detected Player.");
        yield return new WaitForSeconds(data.reactionCooldown);
        Debug.Log(name + " shot the player for " + damage + " damage.");
        yield return new WaitForSeconds(data.shootCooldown);
        if (!playerDetected)
        {
            state = EnemyState.PATROL;
        }
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
}
