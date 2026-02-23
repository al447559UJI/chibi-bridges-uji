using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Entered Fall State");
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.controller.Move();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.controller.isGrounded)
        {
            if (player.controller.IsBufferedJumpAvailable())
            {
                player.SwitchState(player.jumpState);
            }
            else if (player.controller.horizontalInput != 0f)
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
