using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space(15)]
    [SerializeField] private BoxCollider2D feetCollider;
    [SerializeField] private PlayerMovementData data;
    [SerializeField] private LayerMask groundLayer;

    private PlayerInput input;
    private Animator animator;
    public Rigidbody2D rb { get; private set; }
    private float lastJumpStartTime;
    private float lastGroundedTime;

    public bool isGrounded { get; private set; }
    public bool isFalling { get; private set; }

    private float feetCollision = 0.05f;
    public int facingDirection { get; private set; } = 1; // 1 = right, -1 = left.
    public bool isKnockbackActive { get; private set; } = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        DebugRegistry.Register("Speed X", () => Math.Truncate(100 * rb.linearVelocityX) / 100);
        DebugRegistry.Register("Speed Y", () => Math.Truncate(100 * rb.linearVelocityY) / 100);
        DebugRegistry.Register("IsGrounded", () => isGrounded);
    }

    void Update()
    {
        // TODO: Not sure if these are correct.
        if (input.horizontal < 0 && facingDirection == 1)
        {
            Flip();
        }
        else if (input.horizontal > 0 && facingDirection == -1)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
        HandleVariableJumpTime();
        HandleGravity();
    }

    public void Move()
    {
        float targetSpeed = input.horizontal * data.maxSpeed;
        float speedDifference = targetSpeed - rb.linearVelocityX;

        // Use acceleration when input exists, deceleration when stopping
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.acceleration : data.deceleration;

        float movement = accelRate * speedDifference * Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(
            rb.linearVelocityX + movement,
            rb.linearVelocityY
        );
    }
    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, data.jumpForce);
        lastJumpStartTime = Time.time;
        input.UnlockHorizontalMovement();
    }

    public bool IsBufferedJumpAvailable()
    {
        return Time.time - input.lastJumpPressedTime <= data.jumpBufferTime;
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            feetCollider.bounds.center,
            feetCollider.size,
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
            rb.linearVelocityY *= data.variableJumpTimeVelocityMultiplier;
        }
    }

    public bool HasExceededVariableJumpTime()
    {
        return Time.time - lastJumpStartTime > data.variableJumpTime;
    }

    private void HandleGravity()
    {
        float absVelocityY = Mathf.Abs(rb.linearVelocityY);
        isFalling = rb.linearVelocityY < 0;

        if (isFalling)
            rb.gravityScale = data.originalGravityScale * data.fastFallMultiplier;
        else
            rb.gravityScale = data.originalGravityScale;

        if (!isGrounded && absVelocityY < data.jumpHangThreshold)
        {
            rb.gravityScale = data.originalGravityScale * data.jumpHangGravityMultiplier;
        }

        rb.linearVelocityY = Mathf.Max(rb.linearVelocityY, -data.maxFallSpeed);
    }

    public bool HasCoyoteTimeRemaining()
    {
        return Time.time - lastGroundedTime <= data.coyoteTimeThreshold;
    }

    private void Flip()
    {
        facingDirection = -facingDirection;
        transform.Rotate(0f, 180f, 0f);
    }

    public void HurtKnockback(int direction)
    {
        if (direction != -1 && direction != 1)
        {
            Debug.LogError($"HurKnockback direction is now allowed (-1, 1), direction = {direction}");
            return;
        }
        else
        {
            StartCoroutine(HandleKnockback());
        }

    }

    public IEnumerator HandleKnockback()
    {
        input.LockJump();
        input.LockHorizontalMovement();
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isKnockbackActive", true); // Turns off via animation events.

        rb.linearVelocity = new Vector2(
        rb.linearVelocityX + -facingDirection * data.knockbackForce,
        rb.linearVelocityY + data.knockbackForce
        );

        yield return new WaitForSeconds(data.knockbackTime);
        input.UnlockJump();
        input.UnlockHorizontalMovement();
    }

    // Triggered via animation events.
    public void ResetKnockbackAnimation()
    {
        animator.SetBool("isKnockbackActive", false);
    }

}
