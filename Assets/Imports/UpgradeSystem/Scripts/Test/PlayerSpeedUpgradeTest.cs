using UpgradeSystem.Core;

namespace UpgradeSystem.Scripts.Test
{
    public class PlayerSpeedUpgradeTest : UpgradeBehaviour<int, float>
    {
        protected override BaseUpgradeCalculator<int, float> Calculator
        {
            get => m_Calculator;
            set => m_Calculator = (PlayerSpeedUpgradeCalculator) value;
        }

        protected override bool CanAfford
        {
            get => DummyCoin.HasEnough(Calculator.CurrentCost);
        }

        protected override bool CanAffordNext
        {
            get => DummyCoin.HasEnough(Calculator.NextCost);
        }


        private PlayerSpeedUpgradeCalculator m_Calculator;
    

        protected override void Start()
        {
            Initialize(new PlayerSpeedUpgradeCalculator());
            base.Start();
        }
    
    
        protected override void PayCost(int cost)
        {
            DummyCoin.TryRemove(cost);
        }
    }
}
