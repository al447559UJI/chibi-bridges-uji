using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData data;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private BoxCollider2D bodyCollider;
    private Vector2 bodySize;

    private float currentDirectionTimer;
    private int currentHealth;

    private bool isGrounded;
    private int currentDirection = 1; // 1 = Right, -1 = Left.
    private float feetCollision = 0.05f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

        DebugRegistry.Register("IsGrounded Enemy", () => isGrounded);
    }

    void Start()
    {
        bodySize = bodyCollider.bounds.size;
        currentDirectionTimer = data.directionTimer;
        currentHealth = data.health;
    }

    void FixedUpdate()
    {
        Move();
        CheckGrounded();
        UpdateTimer();
        if (isGrounded && IsOnPlatformEdge())
        {
            Flip();
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

        Vector2 bottomLeft  = new Vector2(bounds.min.x, bounds.min.y);
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
}
