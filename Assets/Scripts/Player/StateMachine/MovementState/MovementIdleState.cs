using UnityEngine;

public class MovementIdleState : MovementBaseState
{
    public override MovementStateType Type => MovementStateType.IDLE;

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
        if (player.controller.movement.isFalling)
        {
            player.SwitchState(player.fallState);
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
        if (player.controller.input.horizontal != 0f)
        {
            player.SwitchState(player.moveState);
            return;
        }

    }
}
