using EMRE.Scripts;
using IdleCashSystem.Core;
using UnityEngine;
using UpgradeSystem.Core;

public class PlayerCapacityUpgrade : UpgradeBehaviour<IdleCash, IdleCash>
{
    protected override BaseUpgradeCalculator<IdleCash, IdleCash> Calculator
    {
        get => m_Calculator;
        set => m_Calculator = (PlayerCapacityCalculator) value;
    }


    private PlayerCapacityCalculator m_Calculator;


    protected override bool CanAfford
    {
        get => Balance.HasEnough(Calculator.CurrentCost);
    }

    protected override bool CanAffordNext
    {
        get => Balance.HasEnough(Calculator.NextCost);
    }


    protected override void Start()
    {
        Initialize(new PlayerCapacityCalculator());
        base.Start();
    }


    protected override void PayCost(IdleCash cost)
    {
        Balance.TryRemove(cost);
    }
}
