using UnityEngine;

public class ActionInactiveState : ActionBaseState
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
        if (player.controller.input.isMeleePressed)
        {
            player.SwitchState(player.meleeState);
        }
        else if (player.controller.input.isShootPressed)
        {
            player.SwitchState(player.shootState);
        }
        else if (player.controller.input.buildModePressedThisFrame)
        {
            player.SwitchState(player.buildState);
        }
    }
}