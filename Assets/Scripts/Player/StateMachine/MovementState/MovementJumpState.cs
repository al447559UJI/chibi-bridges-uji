using UnityEngine;

public class MovementJumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager player)
    {
        //Debug.Log("Entered Jump State");
        player.controller.Jump();
    }

    public override void ExitState(MovementStateManager player)
    {
        
    }

    public override void FixedUpdateState(MovementStateManager player)
    {

    }

    public override void UpdateState(MovementStateManager player)
    {
        if (!player.controller.isGrounded)
        {
            player.SwitchState(player.fallState);
        }
    }
}
