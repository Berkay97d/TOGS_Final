using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{

    private PlayerMovement _playerMovement;
    private PlayerStack _playerStack;

    private float offset;
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("hello from the running state");

        _playerMovement = player.GetComponent<PlayerMovement>();
        _playerStack = player.GetComponent<PlayerStack>();
        
        offset = player.transform.GetChild(0).childCount > 0
            ? player.transform.GetChild(0).GetChild(player.transform.GetChild(0).childCount - 1).position.y + 1f
            : 0.25f;
        
        player.GetComponent<Animator>().SetBool("running", true);
        player.GetComponent<Animator>().SetBool("attack", false);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        CheckRay(player);

        if (Input.touchCount <= 0)
        {
            player.SwitchState(player.idleState);
        }
    }

    private void CheckRay(PlayerStateManager player)
    {
        if (Physics.Raycast(player.transform.position, player.transform.forward, out var hit, _playerMovement.rayDistance))
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
        }
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collider collider)
    {
        if (collider.CompareTag("Stump"))
        {
            collider.transform.DOKill();
            
            _playerStack.stumpList.Add(collider.transform);
            
            player.transform.GetChild(0).gameObject.SetActive(true);

            collider.transform.SetParent(player.transform.GetChild(0));
            collider.transform.localEulerAngles = new Vector3(0, 0, 90f);

            collider.transform.localPosition = new Vector3(-0.8f, offset, 0);
            offset += 1;
            
            player.SwitchState(player.carryState);
        }
    }
}
