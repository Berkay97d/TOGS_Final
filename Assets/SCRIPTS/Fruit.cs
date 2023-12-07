using System.Collections;
using UnityEngine;

namespace EMRE.Scripts
{
    public class Fruit : Item
    {
        [SerializeField] private ItemData juiceData;


        protected override void Start()
        {
            base.Start();
            StartCoroutine(DestroyAfterDelay(30f));
        }

        IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (transform.parent == null)
            {
                Destroy(gameObject);
            }
        }
        
        public Juice TurnToJuice()
        {
            var juice = Instantiate(juiceData.prefab);
            return (Juice) juice;
        }
    }
}