using UnityEngine;

public class PlayerHarvestState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        
        base.EnterState(player);
        Debug.Log("hello from the harvest state");
        
        
        player._playerAnimator.HarvestAnimation();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        base.UpdateState(player);
        Debug.Log("harvesting...");

       /* if (Physics.Raycast(_playerAttack.transform.position, _playerAttack.transform.forward, out var hit, 1.5f))
        {
            Debug.DrawRay(_playerAttack.transform.position, _playerAttack.transform.forward*1.5f, Color.cyan);
            
            
            if (hit.collider.CompareTag("Tree"))
            {
                if (HitOverlap())
                {
                    
                }
                else
                {
                }
            }
            
        }
        else
        {
            Debug.DrawRay(_playerAttack.transform.position, _playerAttack.transform.forward*1.5f, Color.magenta);
            player.SwitchState(player.runningState);
        }
        */
    }
    
    private float hitDelay=0;
    /*private bool HitOverlap()
    {
        hitDelay += Time.deltaTime;
        if (hitDelay >= 1/_playerAttack.attackSpeed)
        {
           Collider[] hitColliders = 
               Physics.OverlapBox(
                   _playerAttack.attackPoint.position + _playerAttack.attackPointOffset, 
                   _playerAttack.attackRange, 
                   Quaternion.identity, 
                   _playerAttack.combatMask);
           
           Debug.Log(hitColliders.Length);
           if (hitColliders.Length > 0)
           {
               foreach (Collider collider in hitColliders) 
               {
                   if (collider.CompareTag("Tree"))
                   {
                       Debug.Log("HIT THE TREE");
                       collider.SendMessage("TakeDamage", _playerAttack.attackDamage);

                       break;
                   }
               }
           }
           else
           {
               return false;
           }
           hitDelay = 0;
        }
        return true;
    }*/
    

    public override void OnTriggerEnter(PlayerStateManager player, Collider collider)
    {
    }

    public override void OnTriggerExit(PlayerStateManager player, Collider collider)
    {
        if (collider.TryGetComponent(out FarmLand _))
        {
            player.SwitchState(player.idleState);
        }
    }
}