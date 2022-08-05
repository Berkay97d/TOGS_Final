using UnityEngine;

public class PlayerPlantState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        base.EnterState(player);
        Debug.Log("hello from the Plant state");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        base.UpdateState(player);
        player._playerAnimator.PlantAnimation();
    }
    
    public override void OnTriggerExit(PlayerStateManager player, Collider collider)
    {
        if (collider.TryGetComponent(out FarmLand _))
        {
            player.SwitchState(player.idleState);
        }
    }
}