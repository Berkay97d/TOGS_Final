using System;
using UnityEngine;

namespace EMRE.Scripts
{
    public class Planter : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out FarmTile farmTile))
            {
                if (farmTile.TryPlantSeed())
                {
                    
                }
            }
        }
    }
}