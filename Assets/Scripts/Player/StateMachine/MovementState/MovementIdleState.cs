using UnityEngine;

public class MovementIdleState : MovementBaseState
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
        if (player.controller.input.horizontalInput != 0f)
        {
            player.SwitchState(player.moveState);
        }
        if (player.controller.movement.isFalling)
        {
            player.SwitchState(player.fallState);
        }
        if (player.controller.input.wantsJump)
        {
            if (player.controller.movement.isGrounded)
            {
                player.SwitchState(player.jumpState);
            }
            else if (player.controller.movement.HasCoyoteTimeRemaining())
            {
                //TODO: Test this scenario
                player.SwitchState(player.jumpState);
            }
        }

    }
}
