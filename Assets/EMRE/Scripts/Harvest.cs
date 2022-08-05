using EMRE.Scripts;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Harvestable harvestable))
        {
            harvestable.Harvest();
            Destroy(other.gameObject);
        }
    }
}
