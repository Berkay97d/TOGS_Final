using EMRE.Scripts;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Harvestable harvestable))
        {
            if (harvestable.TryHarvest())
            {
                
            }
        }
    }
}