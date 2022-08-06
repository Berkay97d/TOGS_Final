using System.Linq;
using EMRE.Scripts;
using IdleCashSystem.Core;
using LazyDoTween.Core;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        base.EnterState(player);
        player._playerAnimator.RunAnimation();
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
        player.GetComponent<Rigidbody>().velocity = new Vector3(
            player._playerMovement._floatingJoystick.Horizontal * player._playerMovement.horizontalMovementSpeed, 
            0, 
            player._playerMovement._floatingJoystick.Vertical * player._playerMovement.verticalMovementSpeed);
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
            player._playerStackTransition.JuicerTankMoving();
            juiceCreationPoint.transform.parent.GetComponent<DoLazyMove>().Play();
        }
        else if (collider.TryGetComponent(out JuiceSellPoint juiceSellPoint))
        {
            if (!Inventory.IsEmpty())
            {
                player._cinemachineController.JuicesSelling();
        
                player._playerStackTransition.JuicesMovingToShip();
            }
        }
    }
    public override void OnTriggerExit(PlayerStateManager player, Collider collider)
    {
        if (collider.TryGetComponent(out JuiceCreationPoint juiceCreationPoint))
        {
            player._playerStackTransition.StopJuicerTankMoving();
            juiceCreationPoint.transform.parent.GetComponent<DoLazyMove>().Kill();  
        }
        else if (collider.TryGetComponent(out JuiceSellPoint juiceSellPoint))
        {
            player._cinemachineController.InitialPriority();
            
            player._playerStackTransition.StopJuicesMovingToShip();
        }
    }
    
    
    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Item item))
        {
            if (item is MoneyBundle moneyBundle)
            {
                moneyBundle.Deposit();
            }
            
            else if (!Inventory.IsFull(player._playerStackTransition.bagSize))
            {
                Inventory.StackItem(item.Data, IdleCash.One);

                player._playerStackTransition.CollectItem(item.transform);
            }
        }
    }
}