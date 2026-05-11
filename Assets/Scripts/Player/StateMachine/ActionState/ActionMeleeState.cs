using UnityEngine;

public class ActionMeleeState : ActionBaseState
{
    public override ActionStateType Type => ActionStateType.MELEE;

    public override void EnterState(ActionStateManager player)
    {
        player.controller.actions.MeleeAttack();
        player.controller.input.LockHorizontalMovement(true);
        player.controller.input.LockJump(true);
    }

    public override void ExitState(ActionStateManager player)
    {
        player.controller.actions.HideMeleeVisual();
        player.controller.input.LockHorizontalMovement(false);
        player.controller.input.LockJump(false);
    }

    public override void FixedUpdateState(ActionStateManager player)
    {

    }

    public override void UpdateState(ActionStateManager player)
    {

        if (player.controller.input.isMeleePressed)
        {
            if (!player.controller.actions.IsMeleeAnimationPlaying())
            {
                player.controller.actions.MeleeAttack();
            }
        } else
        {
            if (!player.controller.actions.IsMeleeAnimationPlaying())
            {
                player.SwitchState(player.inactiveState);
                return;
            }
        }
    }
}