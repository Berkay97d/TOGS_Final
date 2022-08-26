using UnityEngine;

namespace EMRE.Scripts
{
    public class Fruit : Item
    {
        [SerializeField] private ItemData juiceData;


        public Juice TurnToJuice()
        {
            var juice = Instantiate(juiceData.prefab);
            return (Juice) juice;
        }
    }
}