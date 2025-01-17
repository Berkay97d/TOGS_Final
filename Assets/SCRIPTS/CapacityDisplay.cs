using Helpers;
using IdleCashSystem.Core;
using TMPro;
using UnityEngine;
using UpgradeSystem.Core;

namespace EMRE.Scripts
{
    public class CapacityDisplay : Scenegleton<CapacityDisplay>
    {
        private const string Middle = "in";
        
        
        [SerializeField] private TMP_Text capacityField;
        [SerializeField] private Color emptyColor;
        [SerializeField] private Color fullColor;


        private IdleCash m_Capacity;
        private IdleCash m_ItemCount;


        public void OnCapacityLoaded(UpgradeResponse<IdleCash, IdleCash> response)
        {
            m_Capacity = response.currentValue;
            UpdateDisplay();
        }

        public void OnCapacityUpgraded(UpgradeResponse<IdleCash, IdleCash> response)
        {
            m_Capacity = response.nextValue;
            UpdateDisplay();
        }

        public static void OnItemCountChanged(IdleCash count)
        {
            Instance.m_ItemCount = count;
            Instance.UpdateDisplay();
        }


        private void UpdateDisplay()
        {
            var rate = m_ItemCount / m_Capacity;
            capacityField.color = Color.Lerp(emptyColor, fullColor, rate);
            capacityField.text = $"{m_ItemCount} {Middle} {m_Capacity}";
        }
    }
}