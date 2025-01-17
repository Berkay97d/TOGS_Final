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
        if (Input.touchCount > 0 && player._playerMovement.IsEnoughJoystickInputForMovement() && !CinemachineController.isTutorialCamActive)
            player.SwitchState(player.runState);
        else
            player._playerMovement.rigidbody.velocity = Vector3.zero;
    }
}
