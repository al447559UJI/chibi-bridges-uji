using UnityEngine;

public class ActionInactiveState : ActionBaseState
{
    public override ActionStateType Type => ActionStateType.INACTIVE;

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
            if (player.controller.movement.isGrounded)
            {
                player.SwitchState(player.meleeState);
            }
            else
            {
                player.SwitchState(player.airMeleeState);
            }
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