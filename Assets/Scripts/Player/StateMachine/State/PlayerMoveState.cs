using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Player entered Move State");
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
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

    public override void UpdateState(PlayerStateManager player)
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
