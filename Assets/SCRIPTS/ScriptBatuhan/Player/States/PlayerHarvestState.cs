using UnityEngine;

public class PlayerHarvestState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        
        base.EnterState(player);
        player._playerAnimator.HarvestAnimation();
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider collider)
    {
        base.OnTriggerExit(player, collider);
        if (collider.TryGetComponent(out FarmLand _))
        {
            player.SwitchState(player.idleState);
        }
    }
}