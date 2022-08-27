using System;
using UnityEngine;

namespace EMRE.Scripts
{
    public class Planter : MonoBehaviour
    {
        [SerializeField] private PlayerStateManager playerStateManager;

        private void OnTriggerStay(Collider other)
        {
            if (playerStateManager.currentState == playerStateManager.plantState && other.TryGetComponent(out FarmTile farmTile))
            {
                if (farmTile.TryPlantSeed())
                {
                    
                }
            }
        }
    }
}