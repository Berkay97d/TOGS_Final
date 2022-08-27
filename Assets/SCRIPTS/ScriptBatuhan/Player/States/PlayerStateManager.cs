using System.Collections;
using EMRE.Scripts;
using IdleCashSystem.Core;
using UnityEngine;
using UpgradeSystem.Core;

public class PlayerStateManager : MonoBehaviour
{
    public CinemachineController _cinemachineController;

    public FarmLand farmLand;
    
    [HideInInspector] public PlayerController _playerController;
    [HideInInspector] public PlayerAnimator _playerAnimator;
    [HideInInspector] public PlayerMovement _playerMovement;
    [HideInInspector] public PlayerStackTransition _playerStackTransition;
    
    [HideInInspector] public PlayerBaseState currentState;
    public string currStateName;

    public PlayerIdleState idleState = new PlayerIdleState();
    
    public PlayerRunState runState = new PlayerRunState();
    public PlayerHarvestState harvestState = new PlayerHarvestState();
    public PlayerPlantState plantState = new PlayerPlantState();
    
    public float SpeedMultiplier { get; private set; }
    public float MagnetRadius { get; private set; }

    private void Start()
    {
        InitilizeReferences();
        
        SetSuperState();
        
        currentState = idleState;
        currentState.EnterState(this);
    }


    public void OnSpeedMultiplierLoaded(UpgradeResponse<IdleCash, float> response)
    {
        ChangeSpeedMultiplier(response.currentValue);
    }
    
    public void OnSpeedMultiplierUpgrade(UpgradeResponse<IdleCash, float> response)
    {
        ChangeSpeedMultiplier(response.nextValue);
    }
    
    private void ChangeSpeedMultiplier(float multiplier)
    {
        SpeedMultiplier = multiplier;
    }
    
    public void OnMagnetRadiusLoaded(UpgradeResponse<IdleCash, float> response)
    {
        ChangeMagnetRadius(response.currentValue);
    }
    
    public void OnMagnetRadiusUpgraded(UpgradeResponse<IdleCash, float> response)
    {
        ChangeMagnetRadius(response.nextValue);
    }
    
    private void ChangeMagnetRadius(float radius)
    {
        MagnetRadius = radius;
    }
    
    
    private void InitilizeReferences()
    {
        _playerController = GetComponent<PlayerController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerStackTransition = GetComponent<PlayerStackTransition>();
    }
    
    private void SetSuperState()
    {
        harvestState.SetSuperState(runState);
        plantState.SetSuperState(runState);
    }

    private void FixedUpdate()
    {
        currentState.OnFixedUpdate(this);
        CheckMagnet(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
        currStateName = currentState.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    private void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(this, collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        currentState.OnTriggerStay(this, collider);
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

    public float StateMovementSpeedEffect()
    {
        return IsActiveState(plantState) || IsActiveState(harvestState) ? 0.75f : 1f;
    }
    
    private void CheckMagnet(PlayerStateManager player)
    {
        var origin = player.transform.position;
        var colliders = Physics.OverlapSphere(origin, player.MagnetRadius);
        
        if (colliders.Length <= 0) return;

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Item item))
            {
                if (item.CanCollect)
                {
                    if (item is MoneyBundle moneyBundle)
                    {
                        moneyBundle.Deposit();
                    }
                
                    else if (!Inventory.IsFull(player._playerStackTransition.BagSize))
                    {
                        Inventory.StackItem(item.Data, IdleCash.One);

                        player._playerStackTransition.CollectItem(item.transform);
                    }
                }
            }
        }
    }

    public IEnumerator ActivateHarvestStateSelectionButtonWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _playerController.ActivateHarvestStateButton();
    }
    
}
