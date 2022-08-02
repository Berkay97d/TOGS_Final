using UnityEngine;
using UpgradeSystem.Core;

namespace UpgradeSystem.Scripts.Test
{
    public class PlayerSpeedUpgradeCalculator : BaseUpgradeCalculator<int, float>
    {
        public override int CurrentCost
        {
            get => Mathf.RoundToInt(Mathf.Lerp(StartCost, EndCost, CurrentCostT));
        }

        public override int NextCost
        {
            get => Mathf.RoundToInt(Mathf.Lerp(StartCost, EndCost, NextCostT));
        }

        public override float CurrentValue
        {
            get => Mathf.Lerp(StartValue, EndValue, CurrentValueT);
        }

        public override float NextValue
        {
            get => Mathf.Lerp(StartValue, EndValue, NextValueT);
        }
    }
}