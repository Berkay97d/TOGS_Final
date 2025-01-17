using System.Collections;
using UnityEngine;

public class PlayerPlantState : PlayerBaseState
{
    public override void UpdateState(PlayerStateManager player)
    {
        base.UpdateState(player);
        player._playerAnimator.PlantAnimation();
    }

    public override void OnTriggerStay(PlayerStateManager player, Collider collider)
    {
        base.OnTriggerStay(player, collider);
        if (collider.TryGetComponent(out FarmLand farmLand))
        {
            switch (farmLand.State)
            {
                case FarmLandState.Growing:
                    player.harvestStateActivationCoroutine = player.ActivateHarvestStateSelectionButtonWithDelay(farmLand.CurrentGrowthUpgradeData.growthDuration);
                    player.StartCoroutine(player.harvestStateActivationCoroutine);
                    
                    player.SwitchState(player.idleState);
                    break;
            }
        }
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider collider)
    {
        base.OnTriggerExit(player, collider);
        if (collider.TryGetComponent(out FarmLand farmLand))
        {
            switch (farmLand.State)
            {
                case FarmLandState.Growing:
                    player.StopCoroutine(player.harvestStateActivationCoroutine);
                    break;
            }
            
            player.SwitchState(player.idleState);
        }
    }
}