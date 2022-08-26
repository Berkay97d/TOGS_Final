using IdleCashSystem.Core;
using UnityEngine;
using UpgradeSystem.Core;

namespace EMRE.Scripts
{
    public class PlayerSpeedCalculator : BaseUpgradeCalculator<IdleCash, float>
    {
        public override IdleCash CurrentCost => IdleCash.Lerp(StartCost, EndCost, CurrentCostT);
        public override IdleCash NextCost => IdleCash.Lerp(StartCost, EndCost, NextCostT);
        public override float CurrentValue => Mathf.Lerp(StartValue, EndValue, CurrentValueT);
        public override float NextValue => Mathf.Lerp(StartValue, EndValue, NextValueT);
    }
}