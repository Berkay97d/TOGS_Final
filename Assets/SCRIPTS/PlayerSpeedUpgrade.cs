using EMRE.Scripts;
using IdleCashSystem.Core;
using UpgradeSystem.Core;

public class PlayerSpeedUpgrade : UpgradeBehaviour<IdleCash, float>
{
    protected override BaseUpgradeCalculator<IdleCash, float> Calculator
    {
        get => m_Calculator;
        set => m_Calculator = (PlayerSpeedCalculator) value;
    }

    protected override bool CanAfford => Balance.HasEnough(Calculator.CurrentCost);
    protected override bool CanAffordNext => Balance.HasEnough(Calculator.NextCost);


    private PlayerSpeedCalculator m_Calculator;


    protected override void Start()
    {
        Initialize(new PlayerSpeedCalculator());
        base.Start();
    }


    protected override void PayCost(IdleCash cost)
    {
        Balance.TryRemove(cost);
    }
}
