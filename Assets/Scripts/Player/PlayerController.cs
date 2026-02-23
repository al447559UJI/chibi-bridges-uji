using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private float feetCollision = 0.05f;

    private InputAction moveAction;
    private InputAction jumpAction;
    public bool wantsJump;
    private bool isJumpPressed;

    [SerializeField] private PlayerData playerData;

    [SerializeField] private double debugCurrentSpeedX;
    [SerializeField] private double debugCurrentSpeedY;


    public bool isGrounded { get; private set; }
    public bool isFalling { get; private set; }
    public float lastJumpPressedTime;
    private float lastJumpStartTime;
    private float lastGroundedTime;
    public float horizontalInput;
    public Rigidbody2D rb { get; private set; }
    [SerializeField] private BoxCollider2D bodyCollider;
    private Vector2 bodySize;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        bodySize = bodyCollider.bounds.size;
        bodySize.y -= feetCollision;
    }

    void Update()
    {
        horizontalInput = moveAction.ReadValue<Vector2>().x;
        wantsJump = jumpAction.WasPressedThisFrame();
        isJumpPressed = jumpAction.IsPressed();

        if (wantsJump)
        {
            lastJumpPressedTime = Time.time;
        }

    }

    void FixedUpdate()
    {
        CheckGrounded();
        HandleVariableJumpTime();
        HandleGravity();

        debugCurrentSpeedX = Math.Truncate(100 * rb.linearVelocityX) / 100;
        debugCurrentSpeedY = Math.Truncate(100 * rb.linearVelocityY) / 100;
    }

    public void Move()
    {
        float targetSpeed = horizontalInput * playerData.maxSpeed;
        float speedDifference = targetSpeed - rb.linearVelocityX;

        // Use acceleration when input exists, deceleration when stopping
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.acceleration : playerData.deceleration;

        float movement = accelRate * speedDifference * Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(
            rb.linearVelocityX + movement,
            rb.linearVelocityY
        );
    }
    public void Jump()
    {
        //Debug.Log("Executed Jump");
        rb.linearVelocity = new Vector2(rb.linearVelocityX, playerData.jumpForce);
        lastJumpStartTime = Time.time;
    }

    public bool IsBufferedJumpAvailable()
    {
        return Time.time - lastJumpPressedTime <= playerData.jumpBufferTime;
    }

    public bool HasExceededVariableJumpTime()
    {
        return Time.time - lastJumpStartTime > playerData.variableJumpTime;
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
        if (rb.linearVelocityY > 0 && !isJumpPressed && HasExceededVariableJumpTime())
        {
            rb.linearVelocityY *= playerData.variableJumpTimeVelocityMultiplier;
        }
    }

    private void HandleGravity()
    {
        float absVelocityY = Mathf.Abs(rb.linearVelocityY);
        isFalling = rb.linearVelocityY < 0;

        if (isFalling)
            rb.gravityScale = playerData.originalGravityScale * playerData.fastFallMultiplier;
        else
            rb.gravityScale = playerData.originalGravityScale;

        if (!isGrounded && absVelocityY < playerData.jumpHangThreshold)
        {
            rb.gravityScale = playerData.originalGravityScale * playerData.jumpHangGravityMultiplier;
        }

        rb.linearVelocityY = Mathf.Max(rb.linearVelocityY , -playerData.maxFallSpeed);
    }

    public bool HasCoyoteTimeRemaining()
    {
        return Time.time - lastGroundedTime <= playerData.coyoteTimeThreshold;
    }

}