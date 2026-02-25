using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementData movementData;
    [SerializeField] private LayerMask groundLayer;

    public bool isGrounded { get; private set; }
    public bool isFalling { get; private set; }

    public Rigidbody2D rb { get; private set; }
    private BoxCollider2D bodyCollider;
    private float feetCollision = 0.05f;
    private float lastJumpStartTime;
    private float lastGroundedTime;
    private Vector2 bodySize;

    private PlayerInput input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        input = GetComponent<PlayerInput>();

        DebugRegistry.Register("Speed X", () => Math.Truncate(100 * rb.linearVelocityX) / 100);
        DebugRegistry.Register("Speed Y", () => Math.Truncate(100 * rb.linearVelocityY) / 100);
    }

    void Start()
    {
        bodySize = bodyCollider.bounds.size;
        bodySize.y -= feetCollision;
    }

    void FixedUpdate()
    {
        CheckGrounded();
        HandleVariableJumpTime();
        HandleGravity();
    }

    public void Move()
    {
        float targetSpeed = input.horizontalInput * movementData.maxSpeed;
        float speedDifference = targetSpeed - rb.linearVelocityX;

        // Use acceleration when input exists, deceleration when stopping
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? movementData.acceleration : movementData.deceleration;

        float movement = accelRate * speedDifference * Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(
            rb.linearVelocityX + movement,
            rb.linearVelocityY
        );
    }
    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, movementData.jumpForce);
        lastJumpStartTime = Time.time;
    }

    public bool IsBufferedJumpAvailable()
    {
        return Time.time - input.lastJumpPressedTime <= movementData.jumpBufferTime;
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

        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }
    }

    private void HandleVariableJumpTime()
    {
        if (rb.linearVelocityY > 0 && !input.isJumpPressed && HasExceededVariableJumpTime())
        {
            rb.linearVelocityY *= movementData.variableJumpTimeVelocityMultiplier;
        }
    }

    public bool HasExceededVariableJumpTime()
    {
        return Time.time - lastJumpStartTime > movementData.variableJumpTime;
    }

    private void HandleGravity()
    {
        float absVelocityY = Mathf.Abs(rb.linearVelocityY);
        isFalling = rb.linearVelocityY < 0;

        if (isFalling)
            rb.gravityScale = movementData.originalGravityScale * movementData.fastFallMultiplier;
        else
            rb.gravityScale = movementData.originalGravityScale;

        if (!isGrounded && absVelocityY < movementData.jumpHangThreshold)
        {
            rb.gravityScale = movementData.originalGravityScale * movementData.jumpHangGravityMultiplier;
        }

        rb.linearVelocityY = Mathf.Max(rb.linearVelocityY, -movementData.maxFallSpeed);
    }

    public bool HasCoyoteTimeRemaining()
    {
        return Time.time - lastGroundedTime <= movementData.coyoteTimeThreshold;
    }
}
