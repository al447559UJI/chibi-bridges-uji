using UnityEngine;

public class MovementFallState : MovementBaseState
{
    public override void EnterState(MovementStateManager player)
    {

    }

    public override void ExitState(MovementStateManager player)
    {

    }

    public override void FixedUpdateState(MovementStateManager player)
    {
        player.controller.movement.Move();
    }

    public override void UpdateState(MovementStateManager player)
    {
        if (player.controller.movement.isGrounded)
        {
            if (player.controller.movement.IsBufferedJumpAvailable())
            {
                player.SwitchState(player.jumpState);
            }
            else if (player.controller.input.horizontalInput != 0f)
            {
                player.SwitchState(player.moveState);
            }
            else
            {
                player.SwitchState(player.idleState);
            }
        }
    }
}
