using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerCarryState carryState = new PlayerCarryState();
    public PlayerRunningState runningState = new PlayerRunningState();
    public PlayerCarryRunningState carryRunningState = new PlayerCarryRunningState();
    public PlayerMeleeAttackState meleeAttackState = new PlayerMeleeAttackState();

    private void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(this, collider);
    }
    

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
