using UnityEngine;

public class ActionBuildState : ActionBaseState
{
    public override ActionStateType Type => ActionStateType.BUILD;

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
            if (player.controller.movement.isGrounded)
            {
                player.SwitchState(player.meleeState);
                return;
            }
            else
            {
                player.SwitchState(player.airMeleeState);
                return;
            }
        }
        else if (player.controller.input.isShootPressed)
        {
            player.SwitchState(player.shootState);
            return;
        }
        else if (player.controller.input.buildModePressedThisFrame)
        {
            player.controller.actions.Build();
            player.SwitchState(player.inactiveState);
            return;
        }
    }
}