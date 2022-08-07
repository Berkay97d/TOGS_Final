using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        base.EnterState(player);
        player._playerAnimator.IdleAnimation();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.touchCount > 0 && !CinemachineController.isTutorialCamActive)
        {
            player.SwitchState(player.runState);
        }
    }
}
