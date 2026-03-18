using UnityEngine;

public class ActionAirMeleeState : ActionBaseState
{
    public override void EnterState(ActionStateManager player)
    {
        player.controller.actions.AirMeleeAttack();
    }

    public override void ExitState(ActionStateManager player)
    {
        player.controller.actions.HideMeleeVisual();

    }

    public override void FixedUpdateState(ActionStateManager player)
    {

    }

    public override void UpdateState(ActionStateManager player)
    {
        if (player.controller.movement.isGrounded)
        {
            player.SwitchState(player.inactiveState);
        }
    }
}