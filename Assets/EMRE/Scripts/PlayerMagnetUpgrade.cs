using System;
using EMRE.Scripts;
using IdleCashSystem.Core;
using UnityEngine;
using UpgradeSystem.Core;

public class PlayerMagnetUpgrade : UpgradeBehaviour<IdleCash, float>
{
    protected override BaseUpgradeCalculator<IdleCash, float> Calculator
    {
        get => m_Calculator;
        set => m_Calculator = (PlayerMagnetCalculator) value;
    }

    protected override bool CanAfford => Balance.HasEnough(Calculator.CurrentCost);
    protected override bool CanAffordNext => Balance.HasEnough(Calculator.NextCost);


    private PlayerMagnetCalculator m_Calculator;
    

    protected override void Start()
    {
        Initialize(new PlayerMagnetCalculator());
        base.Start();
    }


    protected override void PayCost(IdleCash cost)
    {
        Balance.TryRemove(cost);
    }
}
