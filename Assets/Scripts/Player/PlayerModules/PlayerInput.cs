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

    public float lastJumpPressedTime { get; private set; } = -1; // Prevent auto-jump right after loading scene.
    public float lastMeleePressedTime { get; private set; }
    public float lastShootPressedTime { get; private set; }
    public float lastBuildModePressedTime { get; private set; }

    private bool lockHorizontal = false;
    private bool lockJump = false;
    private bool lockActions = false;

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

        if (lockActions && lockHorizontal && lockJump) return;

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

    public void LockHorizontalMovement(bool value)
    {
        lockHorizontal = value;
    }

    public void LockJump(bool value)
    {
        lockJump = value;
    }

    public void LockActions(bool value)
    {
        lockActions = value;
    }

    public void LockInput(bool value)
    {
        LockHorizontalMovement(value);
        LockJump(value);
        LockActions(value);
    }
}
