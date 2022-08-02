using System;
using UnityEngine;

namespace UpgradeSystem.Core
{
    [Serializable]
    public abstract class BaseUpgradeCalculator<TCost, TValue>
    {
        public int Level
        {
            get => PlayerPrefs.GetInt(SaveKey, 1);
            private set => PlayerPrefs.SetInt(SaveKey, value);
        }

        public int LevelIndex => Level - 1;
        
        public bool IsMaxed => Level > EndLevel;

        
        public abstract TCost CurrentCost { get; }
        public abstract TCost NextCost { get; }
        public abstract TValue CurrentValue { get; }
        public abstract TValue NextValue { get; }
        
        
        protected TCost StartCost => m_Upgrade.Data.startCost;
        protected TCost EndCost => m_Upgrade.Data.endCost;
        protected float CurrentCostT => CalculateT(LevelIndex, CostCurve);
        protected float NextCostT => CalculateT(Level, CostCurve);

        protected TValue StartValue => m_Upgrade.Data.startValue;
        protected TValue EndValue => m_Upgrade.Data.endValue;
        protected float CurrentValueT => CalculateT(LevelIndex, ValueCurve);
        protected float NextValueT => CalculateT(Level, ValueCurve);


        private string SaveKey => m_Upgrade.Data.saveKey;
        private int StartLevel => m_Upgrade.Data.startLevel;
        private int EndLevel => m_Upgrade.Data.endLevel;
        private AnimationCurve CostCurve => m_Upgrade.Data.costCurve;
        private AnimationCurve ValueCurve => m_Upgrade.Data.valueCurve;


        private UpgradeBehaviour<TCost, TValue> m_Upgrade;


        public void Inject(UpgradeBehaviour<TCost, TValue> upgrade)
        {
            m_Upgrade = upgrade;
        }

        public void IncrementLevel()
        {
            Level++;
        }


        private float CalculateT(int level, AnimationCurve curve = null)
        {
            var difference = EndLevel - StartLevel;
            var t = level / (float) difference;

            if (curve == null) return t;

            return curve.Evaluate(t);
        }
    }
}