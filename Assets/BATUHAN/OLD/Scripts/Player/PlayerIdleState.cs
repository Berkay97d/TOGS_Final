using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("hello from the idle state");
        
        player.GetComponent<Animator>().SetBool("running", false);
        player.GetComponent<Animator>().SetBool("attack", false);
        player.GetComponent<Animator>().SetBool("carry", false);
        player.GetComponent<Animator>().SetBool("carryRunning", false);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.SwitchState(player.runningState);
        }
    }

    public override void OnTriggerEnter(PlayerStateManager player, Collider collider)
    {
        
    }
}
