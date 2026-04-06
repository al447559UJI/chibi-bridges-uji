using UnityEngine;

public class MovementDeathState : MovementBaseState
{
    public override MovementStateType Type => MovementStateType.DEAD;

    public override void EnterState(MovementStateManager player)
    {
        player.controller.movement.Stop();
        player.controller.input.LockInput(true);
    }

    public override void ExitState(MovementStateManager player)
    {

    }

    public override void FixedUpdateState(MovementStateManager player)
    {

    }

    public override void UpdateState(MovementStateManager player)
    {

    }


}
