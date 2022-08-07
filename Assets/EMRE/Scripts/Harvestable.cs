using System.Collections;
using UnityEngine;

namespace EMRE.Scripts
{
    public class Harvestable : MonoBehaviour
    {
        private const float PlantGrowthRate = 0.8f;
        private const float FruitGrowthRate = 1f - PlantGrowthRate;
        
        
        [SerializeField] private ItemData item;
        [SerializeField] private Transform plant;
        [SerializeField] private Transform fruit;


        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public ItemData Data => item;
        
        
        private Vector3 m_PlantSize;
        private Vector3 m_FruitSize;
        private float m_GrowStartTime;
        private bool m_IsGrown;


        public bool IsGrown => m_IsGrown;


        private void Start()
        {
            InitSize();
        }


        public void StartGrowing(float duration)
        {
            StartCoroutine(Growing());
            
            IEnumerator Growing()
            {
                m_GrowStartTime = Time.time;
                
                while (true)
                {
                    var elapsedTime = Time.time - m_GrowStartTime;
                    var growth = elapsedTime / duration;
                    plant.localScale = Vector3.Lerp(Vector3.zero, m_PlantSize, growth / PlantGrowthRate);
                    fruit.localScale = Vector3.Lerp(Vector3.zero, m_FruitSize, (growth - PlantGrowthRate) / FruitGrowthRate);
                    if (growth >= 1f)
                    {
                        break;
                    }

                    yield return null;
                }

                m_IsGrown = true;
            }
        }
        
        
        public bool TryHarvest()
        {
            if (!m_IsGrown) return false;
            
            Harvest();

            return true;
        }


        private void InitSize()
        {
            m_PlantSize = plant.localScale;
            m_FruitSize = fruit.localScale;
            plant.localScale = Vector3.zero;
            fruit.localScale = Vector3.zero;
        }
        
        private void Harvest()
        {
            var newItem = Instantiate(item.prefab);
            newItem.Position = transform.position;
            var force = Random.insideUnitSphere * 5;
            force.y = 10f;
            newItem.Throw(force);
            Destroy(gameObject);
        }
    }
}