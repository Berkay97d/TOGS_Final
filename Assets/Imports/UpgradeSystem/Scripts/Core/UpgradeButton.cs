using TMPro;
using UnityEngine;

namespace UpgradeSystem.Core
{
    public abstract class UpgradeButton<TCost, TValue> : MonoBehaviour
    {
        private const string LevelPrefix = "LVL";
        private const string Maxed = "MAX";


        [Header("Reference")]
        [SerializeField] private RectTransform main;
        [SerializeField] private TMP_Text levelField;
        [SerializeField] private TMP_Text costField;


        public void OnLoad(UpgradeResponse<TCost, TValue> response)
        {
            if (response.isMaxed)
            {
                OnMaxed();
                return;
            }

            if (!response.canAffordNext)
            {
                OnCantAffordNext();
            }
            
            UpdateFields(response.currentLevel, response.currentCost);
        }
        
        public void OnUpgraded(UpgradeResponse<TCost, TValue> response)
        {
            if (response.isMaxed)
            {
                OnMaxed();
                return;
            }
            
            if (!response.canAffordNext)
            {
                OnCantAffordNext();
            }
            
            UpdateFields(response.NextLevel, response.nextCost);
        }
        

        protected virtual void OnMaxed()
        {
            levelField.text = $"{LevelPrefix} {Maxed}";
            costField.text = Maxed;
        }

        protected virtual void OnCantAffordNext()
        {
            
        }
        
        
        private void UpdateFields(int level, TCost cost)
        {
            levelField.text = $"{LevelPrefix} {level}";
            costField.text = cost.ToString();
        }
    }
}