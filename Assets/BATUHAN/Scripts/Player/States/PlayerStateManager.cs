using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [HideInInspector] public PlayerController _playerController;
    [HideInInspector] public PlayerAnimator _playerAnimator;
    [HideInInspector] public PlayerMovement _playerMovement;
    
    
    PlayerBaseState currentState;
    
    public PlayerIdleState idleState = new PlayerIdleState();
    
    public PlayerRunState runState = new PlayerRunState();
    public PlayerHarvestState harvestState = new PlayerHarvestState();
    public PlayerPlantState plantState = new PlayerPlantState();

    private void Start()
    {
        InitilizeReferences();
        
        SetSuperState();
        
        currentState = idleState;
        currentState.EnterState(this);
    }

    private void InitilizeReferences()
    {
        _playerController = GetComponent<PlayerController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    
    private void SetSuperState()
    {
        harvestState.SetSuperState(runState);
        plantState.SetSuperState(runState);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(this, collider);
    }
    private void OnTriggerExit(Collider collider)
    {
        currentState.OnTriggerExit(this, collider);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public bool IsActiveState(PlayerBaseState state)
    {
        return currentState == state;
    }
}
