using UnityEngine;

public class MovementMoveState : MovementBaseState
{
    public override MovementStateType Type => MovementStateType.MOVE;

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
        if (player.controller.movement.isKnockbackActive)
        {
            player.SwitchState(player.hurtState);
            return;
        }
        if (player.controller.input.jumpPressedThisFrame)
        {
            if (player.controller.movement.isGrounded || player.controller.movement.HasCoyoteTimeRemaining())
            {
                player.SwitchState(player.jumpState);
                return;
            }
        }
        // If linear velocity is between deadzone (0.01f) and there isn't horizontal input, switch to Idle
        if (Mathf.Abs(player.controller.movement.rb.linearVelocityX) < 0.01f && player.controller.input.horizontal == 0)
        {
            player.SwitchState(player.idleState);
        }
    }
}
