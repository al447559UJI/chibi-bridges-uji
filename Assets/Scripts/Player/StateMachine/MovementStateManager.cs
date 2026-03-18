using System;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    private MovementBaseState currentState;
    public PlayerController controller;

    public MovementIdleState idleState = new MovementIdleState();
    public MovementMoveState moveState = new MovementMoveState();
    public MovementJumpState jumpState = new MovementJumpState();
    public MovementFallState fallState = new MovementFallState();

    
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
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
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
