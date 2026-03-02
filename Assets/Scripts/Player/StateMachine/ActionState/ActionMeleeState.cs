using UnityEngine;

public class ActionMeleeState : ActionBaseState
{
    public override void EnterState(ActionStateManager player)
    {
        player.controller.actions.PlayMeleeAnimation();
    }

    public override void ExitState(ActionStateManager player)
    {
        player.controller.actions.HideMeleeVisual();
    }

    public override void FixedUpdateState(ActionStateManager player)
    {

    }

    public override void UpdateState(ActionStateManager player)
    {

        if (player.controller.input.isMeleePressed)
        {
            if (!player.controller.actions.IsMeleeActive())
            {
                player.controller.actions.PlayMeleeAnimation();
            }
        } else
        {
            if (!player.controller.actions.IsMeleeActive())
            {
                player.SwitchState(player.inactiveState);
            }
        }
    }
}