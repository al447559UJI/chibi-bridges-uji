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
        player.controller.Move();
    }

    public override void UpdateState(MovementStateManager player)
    {
        if (player.controller.horizontalInput != 0f)
        {
            player.SwitchState(player.moveState);
        }
        if (player.controller.isFalling)
        {
            player.SwitchState(player.fallState);
        }
        if (player.controller.wantsJump)
        {
            if (player.controller.isGrounded)
            {
                player.SwitchState(player.jumpState);
            }
            else if (player.controller.HasCoyoteTimeRemaining())
            {
                //TODO: Test this scenario
                player.SwitchState(player.jumpState);
            }
        }

    }
}
