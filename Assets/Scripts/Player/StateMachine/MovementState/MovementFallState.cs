using UnityEngine;

public class MovementFallState : MovementBaseState
{
    public override MovementStateType Type => MovementStateType.FALL;
    private float enterTime;
    private float minFallDuration = 0.1f;

    public override void EnterState(MovementStateManager player)
    {
        enterTime = Time.time;
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
        if (Time.time - enterTime < minFallDuration)
        {
            return;
        }

        if (player.controller.movement.isGrounded)
        {
            if (player.controller.movement.IsBufferedJumpAvailable())
            {
                player.SwitchState(player.jumpState);
                return;
            }
            else if (player.controller.input.horizontal != 0f)
            {
                player.SwitchState(player.moveState);
                return;
            }
            else
            {
                player.SwitchState(player.idleState);
                return;
            }
        }
    }
}
