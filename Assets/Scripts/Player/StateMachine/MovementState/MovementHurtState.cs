using UnityEngine;

public class MovementHurtState : MovementBaseState
{
    public override MovementStateType Type => MovementStateType.HURT;

    public override void EnterState(MovementStateManager player)
    {

    }

    public override void ExitState(MovementStateManager player)
    {

    }

    public override void FixedUpdateState(MovementStateManager player)
    {

    }

    public override void UpdateState(MovementStateManager player)
    {
        if (!player.controller.movement.isKnockbackActive)
        {
            if (player.controller.movement.isGrounded)
            {
                if (player.controller.input.horizontal != 0f)
                {
                    player.SwitchState(player.moveState);
                }
                else
                {
                    player.SwitchState(player.idleState);
                }
            }
            else
            {
                player.SwitchState(player.fallState);
            }
        }

    }
}
