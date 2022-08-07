using Helpers;
using TMPro;
using UnityEngine;

namespace EMRE.Scripts
{
    public class GrowthSpeedDuration : Scenegleton<GrowthSpeedDuration>
    {
        private const string LevelPrefix = "LVL";
        
        
        [SerializeField] private TMP_Text levelField;
        [SerializeField] private TMP_Text costField;


        public static void UpdateFields(FarmLand farmLand)
        {
            Instance.levelField.text = $"{LevelPrefix} {farmLand.GrowthLevel}";
            Instance.costField.text = farmLand.CurrentGrowthUpgradeData.growthReductionCost.ToString();
        }
    }
}