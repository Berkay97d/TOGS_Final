using EMRE.Scripts;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    // [SerializeField] private PlayerStateManager playerStateManager;
    private void OnTriggerStay(Collider other)
    {
        // if (playerStateManager.currentState != playerStateManager.harvestState) return;
        if (other.TryGetComponent(out Harvestable harvestable))
        {
            if (harvestable.TryHarvest())
            {
                
            }
        }
    }
}