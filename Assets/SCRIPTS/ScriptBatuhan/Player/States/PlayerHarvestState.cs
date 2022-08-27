using System.Linq;
using EMRE.Scripts;
using UnityEngine;

public class PlayerHarvestState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        base.EnterState(player);
        player._playerAnimator.HarvestAnimation();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        base.UpdateState(player);
        // HandlePlayerInteractionArea(player);
    }
    private void HandlePlayerInteractionArea(PlayerStateManager player)
    {
        var origin = player.transform.position;
        var originHeightOffset = player._playerController.overlapHeightOffset * Vector3.up;
        var hitColliders = 
            Physics.OverlapSphere(
                origin+originHeightOffset, 
                player._playerController.overlapRadius, 
                ~player._playerController.ignoredInteractionLayers);

        if (hitColliders.Length > 0)
        {
            foreach (var hitColl in hitColliders)
            {
                if (hitColl.TryGetComponent(out Harvestable harvestable))
                {
                    player._playerAnimator.HarvestAnimation();
                    break;
                }
            }
        }
        else
        {
            player._playerAnimator.IdleAnimation();
        }
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