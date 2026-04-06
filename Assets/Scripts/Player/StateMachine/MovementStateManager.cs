using UnityEngine;
using UnityEngine.Events;

public enum MovementStateType
{
    IDLE,
    MOVE,
    JUMP,
    FALL,
    HURT
}

public class MovementStateManager : MonoBehaviour
{
    private MovementBaseState currentState;
    public PlayerController controller;

    public MovementIdleState idleState = new MovementIdleState();
    public MovementMoveState moveState = new MovementMoveState();
    public MovementJumpState jumpState = new MovementJumpState();
    public MovementFallState fallState = new MovementFallState();
    public MovementHurtState hurtState = new MovementHurtState();

    public UnityEvent<MovementStateType> onStateEntered;
    public UnityEvent<MovementStateType> onStateExited;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        DebugRegistry.Register("Current Movement", () => GetCurrentStateName());
    }

    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        if (currentState == state) return;

        currentState.ExitState(this);
        onStateExited.Invoke(currentState.Type);

        currentState = state;

        state.EnterState(this);
        onStateEntered.Invoke(state.Type);
    }

    public string GetCurrentStateName()
    {
        if (currentState != null)
        {
            return currentState.GetType().Name;
        }
        return "NoState";
    }
}
