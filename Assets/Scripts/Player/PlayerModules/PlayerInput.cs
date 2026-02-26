using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public bool wantsJump { get; private set; }
    public bool IsMeleeAttackPressed { get; private set; }
    public bool isJumpPressed { get; private set; }
    public bool wantsAttack { get; private set; }



    public float lastJumpPressedTime { get; private set; }
    public float lastMeleeAttackTime { get; private set; }

    private bool lockHorizontal = false;
    private bool lockJump = false;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction meleeAttackAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        meleeAttackAction = InputSystem.actions.FindAction("Attack");

        DebugRegistry.Register("IsHorizontal Locked", () => lockHorizontal);

    }

    void Update()
    {
        IsMeleeAttackPressed = meleeAttackAction.IsPressed();
        wantsAttack = meleeAttackAction.WasPressedThisFrame();

        if (lockJump)
        {
            isJumpPressed = false;
            wantsJump = false;
        }
        else
        {
            isJumpPressed = jumpAction.IsPressed();
            wantsJump = jumpAction.WasPressedThisFrame();
        }

        horizontal = lockHorizontal ? 0 : horizontal = moveAction.ReadValue<Vector2>().x;

        if (wantsJump)
        {
            lastJumpPressedTime = Time.time;
        }

        if (wantsAttack)
        {
            lastMeleeAttackTime = Time.time;
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
