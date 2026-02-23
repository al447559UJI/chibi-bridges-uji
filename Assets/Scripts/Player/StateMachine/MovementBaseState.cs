using UnityEngine;

public abstract class MovementBaseState
{
    public abstract void EnterState(MovementStateManager player);
    public abstract void ExitState(MovementStateManager player);
    public abstract void UpdateState(MovementStateManager player);
    public abstract void FixedUpdateState(MovementStateManager player);
}
