using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public bool attackPressedThisFrame { get; private set; }
    public bool jumpPressedThisFrame { get; private set; }
    public bool shootPressedThisFrame { get; private set; }
    public bool buildModePressedThisFrame { get; private set; }
    public bool isMeleePressed { get; private set; }
    public bool isJumpPressed { get; private set; }
    public bool isShootPressed { get; private set; }

    public float lastJumpPressedTime { get; private set; }
    public float lastMeleePressedTime { get; private set; }
    public float lastShootPressedTime { get; private set; }
    public float lastBuildModePressedTime { get; private set; }

    private bool lockHorizontal = false;
    private bool lockJump = false;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction meleeAction;
    private InputAction shootAction;
    private InputAction buildModeAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        meleeAction = InputSystem.actions.FindAction("Melee");
        shootAction = InputSystem.actions.FindAction("Shoot");
        buildModeAction = InputSystem.actions.FindAction("BuildMode");
    }

    void Update()
    {
        isMeleePressed = meleeAction.IsPressed();
        isShootPressed = shootAction.IsPressed();
        shootPressedThisFrame = shootAction.WasPressedThisFrame();
        attackPressedThisFrame = meleeAction.WasPressedThisFrame();
        buildModePressedThisFrame = buildModeAction.WasPressedThisFrame();

        if (lockJump)
        {
            isJumpPressed = false;
            jumpPressedThisFrame = false;
        }
        else
        {
            isJumpPressed = jumpAction.IsPressed();
            jumpPressedThisFrame = jumpAction.WasPressedThisFrame();
        }

        horizontal = lockHorizontal ? 0 : horizontal = moveAction.ReadValue<Vector2>().x;

        if (jumpPressedThisFrame)
        {
            lastJumpPressedTime = Time.time;
        }

        if (attackPressedThisFrame)
        {
            lastMeleePressedTime = Time.time;
        }

        if (shootPressedThisFrame)
        {
            lastShootPressedTime = Time.time;
        }
    }

    public void LockHorizontalMovement()
    {
        lockHorizontal = true;
    }

    public void LockJump()
    {
        lockJump = true;
    }

    public void UnlockHorizontalMovement()
    {
        lockHorizontal = false;
    }

    public void UnlockJump()
    {
        lockJump = false;
    }
}
