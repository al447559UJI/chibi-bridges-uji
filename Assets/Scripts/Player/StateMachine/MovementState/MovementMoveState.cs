using UnityEngine;

public class MovementMoveState : MovementBaseState
{
    public override void EnterState(MovementStateManager player)
    {
        //Debug.Log("Player entered Move State");
    }

    public override void ExitState(MovementStateManager player)
    {

    }

    public override void FixedUpdateState(MovementStateManager player)
    {
        if (player.controller.horizontalInput == 0f)
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
