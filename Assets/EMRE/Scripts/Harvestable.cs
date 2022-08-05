using UnityEngine;

namespace EMRE.Scripts
{
    public class Harvestable : MonoBehaviour
    {
        [SerializeField] private ItemData item;


        public void Harvest()
        {
            var newItem = Instantiate(item.prefab);
            newItem.Throw();
        }
    }
}