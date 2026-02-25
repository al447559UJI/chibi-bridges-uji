using UnityEngine;

public class ActionMeleeState: ActionBaseState
{
    public override void EnterState(ActionStateManager player)
    {
        player.controller.actions.MeleeAttack();
    }

    public override void ExitState(ActionStateManager player)
    {
        
    }

    public override void FixedUpdateState(ActionStateManager player)
    {
        
    }

    public override void UpdateState(ActionStateManager player)
    {
        if (!player.controller.input.IsMeleeAttackPressed)
        {
            player.SwitchState(player.inactiveState);
        }
    }
}