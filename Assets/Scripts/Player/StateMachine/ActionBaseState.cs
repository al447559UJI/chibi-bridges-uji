using UnityEngine;

public abstract class ActionBaseState
{
    public abstract void EnterState(ActionStateManager player);
    public abstract void ExitState(ActionStateManager player);
    public abstract void UpdateState(ActionStateManager player);
    public abstract void FixedUpdateState(ActionStateManager player);
}
