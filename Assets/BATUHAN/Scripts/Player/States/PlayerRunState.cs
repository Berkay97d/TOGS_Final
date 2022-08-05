using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        base.EnterState(player);
        Debug.Log("hello from the run state");
        
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

    private void CheckRay(PlayerStateManager player)
    {
        /*if (Physics.Raycast(player.transform.position, player.transform.forward, out var hit, _playerMovement.rayDistance))
        {
            Debug.DrawRay(player.transform.position, player.transform.forward*_playerMovement.rayDistance, Color.green);
           
            if (hit.collider)
            {
                if (hit.collider.CompareTag("Tree"))
                {
                    Debug.Log("Tree!");
                    player.SwitchState(player.meleeAttackState);
                }
            }
        }
        else
        {
            Debug.DrawRay(player.transform.position, player.transform.forward*_playerMovement.rayDistance, Color.red);
        }*/
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collider collider)
    {
        /*if (collider.CompareTag("Stump"))
        {
            collider.transform.DOKill();
            
            _playerStack.stumpList.Add(collider.transform);
            
            player.transform.GetChild(0).gameObject.SetActive(true);

            collider.transform.SetParent(player.transform.GetChild(0));
            collider.transform.localEulerAngles = new Vector3(0, 0, 90f);

            collider.transform.localPosition = new Vector3(-0.8f, offset, 0);
            offset += 1;
            
            player.SwitchState(player.carryState);
        }*/
        
        if (collider.CompareTag(player._playerController.harvestableAreaTag))
        {
            player.SwitchState(player.harvestState);
        }
        else if (collider.CompareTag(player._playerController.plantableAreaTag))
        {
            player.SwitchState(player.plantState);
        }
    }
}