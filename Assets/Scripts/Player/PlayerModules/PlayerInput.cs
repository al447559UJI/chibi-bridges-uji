using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    public float horizontalInput { get; private set; }
    public bool wantsJump { get; private set; }
    public bool IsMeleeAttackPressed { get; private set; }
    public bool isJumpPressed { get; private set; }

    public float lastJumpPressedTime { get; private set; }
    public float lastMeleeAttackTime { get; private set; }


    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction meleeAttackAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        meleeAttackAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        horizontalInput = moveAction.ReadValue<Vector2>().x;
        wantsJump = jumpAction.WasPressedThisFrame();
        isJumpPressed = jumpAction.IsPressed();
        IsMeleeAttackPressed = meleeAttackAction.IsPressed();
    
    
        if (wantsJump)
        {
            lastJumpPressedTime = Time.time;
        }
    }
}
