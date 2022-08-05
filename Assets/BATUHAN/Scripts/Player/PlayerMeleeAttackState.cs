using UnityEngine;

public class PlayerMeleeAttackState : PlayerBaseState
{

    private PlayerAttack _playerAttack;
    
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("hello from the melee attack state");

        _playerAttack = player.GetComponent<PlayerAttack>();
        
        player.GetComponent<Animator>().SetBool("running", false);
        player.GetComponent<Animator>().SetBool("attack", true);
    }

    public override void UpdateState(PlayerStateManager player)
    {

        if (Physics.Raycast(_playerAttack.transform.position, _playerAttack.transform.forward, out var hit, 1.5f))
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
        
    }
    
    private float hitDelay=0;
    private bool HitOverlap()
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
    }
    

    public override void OnTriggerEnter(PlayerStateManager player, Collider collider)
    {
        
    }
}
