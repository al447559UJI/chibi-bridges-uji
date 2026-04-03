using UnityEngine;

public class ActionShootState : ActionBaseState
{
    public override ActionStateType Type => ActionStateType.SHOOT;

    public override void EnterState(ActionStateManager player)
    {
        player.controller.actions.Shoot();
    }

    public override void ExitState(ActionStateManager player)
    {
        
    }

    public override void FixedUpdateState(ActionStateManager player)
    {
        
    }

    public override void UpdateState(ActionStateManager player)
    {
        if (player.controller.input.isShootPressed)
        {
            player.controller.actions.Shoot();
        } else
        {
            player.SwitchState(player.inactiveState);
        }
    }
}
