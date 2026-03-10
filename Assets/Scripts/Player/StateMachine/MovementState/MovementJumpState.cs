using UnityEngine;

public class MovementJumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager player)
    {
        player.controller.movement.Jump();
    }

    public override void ExitState(MovementStateManager player)
    {
        
    }

    public override void FixedUpdateState(MovementStateManager player)
    {

    }

    public override void UpdateState(MovementStateManager player)
    {
        if (!player.controller.movement.isGrounded)
        {
            player.SwitchState(player.fallState);
        }
    }
}
