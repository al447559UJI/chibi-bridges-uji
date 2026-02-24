using UnityEngine;

public class ActionInactiveState: ActionBaseState
{
    public override void EnterState(ActionStateManager player)
    {

    }

    public override void ExitState(ActionStateManager player)
    {
        
    }

    public override void FixedUpdateState(ActionStateManager player)
    {
        
    }

    public override void UpdateState(ActionStateManager player)
    {
        if (player.controller.IsMeleeAttackPressed)
        {
            player.SwitchState(player.meleeState);
        }
    }
}