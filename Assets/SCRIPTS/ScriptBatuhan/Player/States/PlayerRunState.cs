using System.Numerics;
using EMRE.Scripts;
using LazyDoTween.Core;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

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
        
        if (Input.touchCount > 0 && player._playerMovement.IsEnoughJoystickInputForMovement())
        {
            HandleMovement(player);
            HandleMovementAnimatorSpeed(player);
            HandleRotation(player);
            /*if (Input.GetTouch(0).phase is TouchPhase.Began or TouchPhase.Moved)
            {
                HandleRotation(player);
            }*/
        }
        else
        {
            player._playerMovement.rigidbody.velocity = Vector3.zero;
            if (player.IsActiveState(this))
            {
                player._playerAnimator.SetOriginalAnimatorSpeed();
                player.SwitchState(player.idleState);
            }
            
        }

    }

    private void HandleMovement(PlayerStateManager player)
    {
        player._playerMovement.rigidbody.velocity =
            new Vector3(
                player._playerMovement.HorizontalMovement,
                0,
                player._playerMovement.VerticalMovement);
    }
    
    private void HandleMovementAnimatorSpeed(PlayerStateManager player)
    {
        var magnitudeOfJoystickInput = player._playerMovement.MagnitudeOfJoystickInput();
        
        //Debug.Log(magnitudeOfJoystickInput);
        
        player._playerAnimator.AnimatorSpeed =
            magnitudeOfJoystickInput < 0.5f
                ? magnitudeOfJoystickInput * 1.5f
                : magnitudeOfJoystickInput;
    }

    private void HandleRotation(PlayerStateManager player)
    {
        var angle = Mathf.Atan2(player._playerMovement.HorizontalJoystickInput, player._playerMovement.VerticalJoystickInput) * Mathf.Rad2Deg; 
        player.transform.rotation = Quaternion.Euler(new Vector3(0, angle+180, 0));
    }


    public override void OnTriggerStay(PlayerStateManager player, Collider collider)
    {
        if (collider.TryGetComponent(out FarmLand farmLand))
        {
            /*switch (farmLand.State)
            {
                case FarmLandState.Harvestable:
                    player.SwitchState(player.harvestState);
                    break;
                
                case FarmLandState.Seeding:
                    player.SwitchState(player.plantState);
                    break;
            }*/
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
        
        
        if (collider.TryGetComponent(out FarmLand farmLand))
        {
            switch (farmLand.State)
            {
                case FarmLandState.Harvestable:
                    player._playerController.ActivateHarvestStateButton();
                    break;
                
                case FarmLandState.Seeding:
                    player._playerController.ActivatePlantStateButton();
                    break;
            }
            player._playerController.EnableStateSelectionButton();
        }
        

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
                
                if (!farmLandHub.HasWorker)
                {
                    farmLandHub.EnableWorkerUnlock();
                }
            }
        }
        else if (collider.TryGetComponent(out MoneyCrate moneyCrate))
        {
            if(moneyCrate.moneysParent.childCount > 0)
                moneyCrate.EnableMoneyMultiplierButton();
        }
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider collider)
    {
        if (collider.TryGetComponent(out FarmLand farmLand))
        {
            player._playerController.DisableStateSelectionButton();
        }
        else if (collider.TryGetComponent(out JuiceCreationPoint juiceCreationPoint))
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
            farmLandHub.DisableWorkerUnlock();
        }
        else if (collider.TryGetComponent(out MoneyCrate moneyCrate))
        {
            moneyCrate.DisableMoneyMultiplierButton();
        }
    }
}