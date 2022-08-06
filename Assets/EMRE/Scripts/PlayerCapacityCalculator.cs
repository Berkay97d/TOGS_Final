using IdleCashSystem.Core;
using UpgradeSystem.Core;

namespace EMRE.Scripts
{
    public class PlayerCapacityCalculator : BaseUpgradeCalculator<IdleCash, IdleCash>
    {
        public override IdleCash CurrentCost => IdleCash.Lerp(StartCost, EndCost, CurrentCostT);
        public override IdleCash NextCost => IdleCash.Lerp(StartCost, EndCost, NextCostT);
        public override IdleCash CurrentValue => IdleCash.Lerp(StartValue, EndValue, CurrentValueT);
        public override IdleCash NextValue => IdleCash.Lerp(StartValue, EndValue, NextValueT);
    }
}