using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCarryState : PlayerBaseState
{
    private PlayerMovement _playerMovement;

    private float offset;
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("hello from the carry state");
        
        _playerMovement = player.GetComponent<PlayerMovement>();

        offset = player.transform.GetChild(0).childCount > 0
            ? player.transform.GetChild(0).GetChild(player.transform.GetChild(0).childCount - 1).position.y + 1f
            : 0.25f;
        
        player.GetComponent<Animator>().SetBool("running", false);
        
        player.GetComponent<Animator>().SetBool("carry", true);
        player.GetComponent<Animator>().SetBool("carryRunning", false);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.touchCount > 0)
        {
            player.SwitchState(player.carryRunningState);
        }
        
        CheckRay(player);
    }
    private void CheckRay(PlayerStateManager player)
    {
        if (Physics.Raycast(player.transform.position, player.transform.forward, out var hit, _playerMovement.rayDistance))
        {
            Debug.DrawRay(player.transform.position, player.transform.forward*_playerMovement.rayDistance, Color.green);
           
            if (hit.collider)
            {
                if (hit.collider.CompareTag("House"))
                {
                    Debug.Log("House!");

                    for (int i = 0; i < player.GetComponent<PlayerStack>().stumpList.Count; i++)
                    {
                        player.GetComponent<PlayerStack>().stumpList[i].SetParent(hit.transform.GetChild(1));
                        player.GetComponent<PlayerStack>().stumpList[i].localPosition = Vector3.zero;
                        player.GetComponent<PlayerStack>().stumpList.RemoveAt(i);
                    }

                    if (player.GetComponent<PlayerStack>().stumpList.Count <= 0)
                    {
                        player.transform.GetChild(0).gameObject.SetActive(false);
                        player.SwitchState(player.idleState);
                    }
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
            
            player.GetComponent<PlayerStack>().stumpList.Add(collider.transform);
            
            player.transform.GetChild(0).gameObject.SetActive(true);

            collider.transform.SetParent(player.transform.GetChild(0));
            collider.transform.localEulerAngles = new Vector3(0, 0, 90f);

            collider.transform.localPosition = new Vector3(-0.8f, offset, 0);
            offset += 1;
        }
    }
}
