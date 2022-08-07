using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruitUpgrade : Scenegleton<FruitUpgrade>
{
    private const string LevelPrefix = "LVL";
    
    
    [SerializeField] private TMP_Text levelField;
    [SerializeField] private TMP_Text costField;
    [SerializeField] private Image currentFruit;
    [SerializeField] private Image nextFruit;


    public static void UpdateFields(FarmLand farmLand)
    {
        Instance.levelField.text = $"{LevelPrefix} {farmLand.FruitLevel}";
        Instance.costField.text = farmLand.CurrentUpgradeData.fruitUpgradeCost.ToString();
        Instance.currentFruit.sprite = farmLand.CurrentHarvestable.Data.icon;
        Instance.nextFruit.sprite = farmLand.NextHarvestable.Data.icon;
    }
}
