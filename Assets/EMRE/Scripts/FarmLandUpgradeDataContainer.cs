using System;
using IdleCashSystem.Core;
using UnityEngine;

[CreateAssetMenu]
public class FarmLandUpgradeDataContainer : Container<FarmLandUpgradeData>
{
    
}

[Serializable]
public struct FarmLandUpgradeData
{
    public string name;
    public IdleCash fruitUpgradeCost;
    public IdleCash growthReductionCost;
    public float growthDuration;
}
