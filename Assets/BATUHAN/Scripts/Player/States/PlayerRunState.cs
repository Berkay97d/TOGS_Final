using EMRE.Scripts;
using LazyDoTween.Core;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        base.EnterState(player);
        player._playerAnimator.RunAnimation();

        CinemachineController.StandartPriority();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        base.UpdateState(player);
        
        if (Input.touchCount > 0)
        {
            Movement(player);
            if (Input.GetTouch(0).phase is TouchPhase.Began or TouchPhase.Moved)
            {
                Rotation(player);
            }
        }
        else
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (player.IsActiveState(this))
            {
                player.SwitchState(player.idleState);
            }
            
        }

    }
    
    private void Movement(PlayerStateManager player)
    {
        var stateSpeedEffect = 
            player.IsActiveState(player.plantState) || player.IsActiveState(player.harvestState)
            ? 0.75f
            : 1f;

        player.GetComponent<Rigidbody>().velocity = new Vector3(
            player._playerMovement._floatingJoystick.Horizontal * player._playerMovement.horizontalMovementSpeed * stateSpeedEffect * player.SpeedMultiplier, 
            0, 
            player._playerMovement._floatingJoystick.Vertical * player._playerMovement.verticalMovementSpeed * stateSpeedEffect * player.SpeedMultiplier);
    }

    private void Rotation(PlayerStateManager player)
    {
        var angle = Mathf.Atan2(player._playerMovement._floatingJoystick.Horizontal, player._playerMovement._floatingJoystick.Vertical) * Mathf.Rad2Deg; 
        player.transform.rotation = Quaternion.Euler(new Vector3(0, angle+180, 0));
    }


    public override void OnTriggerStay(PlayerStateManager player, Collider collider)
    {
        if (collider.TryGetComponent(out FarmLand farmLand))
        {
            switch (farmLand.State)
            {
                case FarmLandState.Harvestable:
                    player.SwitchState(player.harvestState);
                    break;
                
                case FarmLandState.Seeding:
                    player.SwitchState(player.plantState);
                    break;
            }
        }
        else if (collider.TryGetComponent(out JuiceCreationPoint juiceCreationPoint))
        {
            if (!Inventory.IsEmpty() && Inventory.HasItemType<Fruit>())
            {
                player._playerStackTransition.JuicerTankMoving();
                juiceCreationPoint.transform.parent.GetComponent<DoLazyMove>().Play();
            }
        }
        else if (collider.TryGetComponent(out JuiceSellPoint juiceSellPoint))
        {
            if (!Inventory.IsEmpty() && Inventory.HasItemType<Juice>())
            {
                CinemachineController.JuicesSelling();
        
                player._playerStackTransition.JuicesMovingToShip();
            }
        }
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collider collider)
    {
        base.OnTriggerEnter(player, collider);

        if (collider.CompareTag("Bridge"))
        {
            InGameAds.OnPassedBridge();
        }

        if (collider.TryGetComponent(out PlayerUpgradeAltar altar))
        {
            altar.Enable();
        }
        else if (collider.TryGetComponent(out FarmLandHub farmLandHub))
        {
            FarmLandUpgrader.Select(farmLandHub);
            FruitUpgrade.UpdateFields(farmLandHub.FarmLand);
            GrowthSpeedDuration.UpdateFields(farmLandHub.FarmLand);
            
            if (farmLandHub.IsLocked)
            {
                farmLandHub.EnableUnlockable();
            }
            else
            {
                farmLandHub.EnableUpgradable();
            }
        }
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider collider)
    {
        if (collider.TryGetComponent(out JuiceCreationPoint juiceCreationPoint))
        {
            player._playerStackTransition.StopJuicerTankMoving();
            //juiceCreationPoint.transform.parent.GetComponent<DoLazyMove>().Kill();  
            
            player._playerStackTransition.SortBagItems();
        }
        else if (collider.TryGetComponent(out JuiceSellPoint juiceSellPoint))
        {
            CinemachineController.StandartPriority();
            
            player._playerStackTransition.StopJuicesMovingToShip();

            player._playerStackTransition.SortBagItems();
        }
        
        if (collider.TryGetComponent(out PlayerUpgradeAltar altar))
        {
            altar.Disable();
        }
        else if (collider.TryGetComponent(out FarmLandHub farmLandHub))
        {
            farmLandHub.DisableUnlockable();
            farmLandHub.DisableUpgradable();
        }
    }
}