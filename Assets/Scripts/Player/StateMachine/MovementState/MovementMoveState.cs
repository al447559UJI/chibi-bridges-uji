using UnityEngine;

public class MovementMoveState : MovementBaseState
{
    public override void EnterState(MovementStateManager player)
    {

    }

    public override void ExitState(MovementStateManager player)
    {

    }

    public override void FixedUpdateState(MovementStateManager player)
    {
        // If linear velocity is between deadzone (0.01f) and there isn't horizontal input, switch to Idle
        if (Mathf.Abs(player.controller.rb.linearVelocityX) < 0.01f && player.controller.horizontalInput == 0)
        {
            player.SwitchState(player.idleState);
        }
        else
        {
            player.controller.Move();
        }
    }

    public override void UpdateState(MovementStateManager player)
    {
        if (player.controller.wantsJump)
        {
            if (player.controller.isGrounded)
            {
                player.SwitchState(player.jumpState);
            }
            else if (player.controller.HasCoyoteTimeRemaining())
            {
                player.SwitchState(player.jumpState);
            }
        }
    }
}
