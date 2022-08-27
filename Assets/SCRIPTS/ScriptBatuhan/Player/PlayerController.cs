using LazyDoTween.Core;
using NaughtyAttributes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private GameObject plantStateButton, harvestStateButton;
    [SerializeField] private DoLazyToggleGroup stateSelectionLazyToggleGroup;

    [Header("Harvest State Interaction Area:")]
    public float overlapHeightOffset;
    public float overlapRadius;
    public LayerMask ignoredInteractionLayers;
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + overlapHeightOffset * Vector3.up, overlapRadius);
    }

    public void ActivatePlantStateButton()
    {
        plantStateButton.SetActive(true);
        harvestStateButton.SetActive(false);
    }

    public void ActivateHarvestStateButton()
    {
        harvestStateButton.SetActive(true);
        plantStateButton.SetActive(false);
    }
    
    [Button()]
    public void EnableStateSelectionButton()
    {
        stateSelectionLazyToggleGroup.Enable();
    }
    [Button()]
    public void DisableStateSelectionButton()
    {
        stateSelectionLazyToggleGroup.Disable();
    }
    
    public void SelectHarvestState() {
        playerStateManager.SwitchState(playerStateManager.harvestState);
        DisableStateSelectionButton();
    }
    public void SelectPlantState() {
        playerStateManager.SwitchState(playerStateManager.plantState);
        DisableStateSelectionButton();
    }
}