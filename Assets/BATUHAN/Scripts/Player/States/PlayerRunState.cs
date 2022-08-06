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
    }
}