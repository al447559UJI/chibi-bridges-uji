using UnityEngine;

public class ActionBuildState: ActionBaseState
{
    public override void EnterState(ActionStateManager player)
    {
        player.controller.actions.ShowPoleIndicator();
    }

    public override void ExitState(ActionStateManager player)
    {
        player.controller.actions.HidePoleIndicator();
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
            player.SwitchState(player.inactiveState);
        }
    }
}