using System.Collections;
using System.Collections.Generic;
using EMRE.Scripts;
using NaughtyAttributes;
using UnityEngine;


public enum FarmLandState
{
    Locked,
    Seeding,
    Growing,
    Harvestable
}


public class FarmLand : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FarmTile farmTilePrefab;
    [SerializeField] private Transform farmTileParent;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private HarvestableContainer harvestableContainer;
    [SerializeField] private FarmLandUpgradeDataContainer upgradeDataContainer;
    [SerializeField] private FarmLandSign sign;
    [SerializeField] private GameObject locks;
    [SerializeField] private bool unlockOnStart;
    
    [Header("Values")]
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private float size;


    private int m_Level = 1;
    private int m_GrowthLevel = 1;


    public bool IsLocked => State == FarmLandState.Locked;

    public FarmLandState State
    {
        get
        {
            if (locks.activeSelf) return FarmLandState.Locked;
            
            foreach (var farmTile in m_FarmTiles)
            {
                var harvestable = farmTile.Harvestable;
                if (harvestable)
                {
                    if (harvestable.IsGrown) return FarmLandState.Harvestable;
                }
            }
            
            foreach (var farmTile in m_FarmTiles)
            {
                var harvestable = farmTile.Harvestable;
                if (!harvestable) return FarmLandState.Seeding;
            }

            return FarmLandState.Growing;
        }
    }

    public Harvestable CurrentHarvestable => harvestableContainer[m_Level - 1];
    public Harvestable NextHarvestable => harvestableContainer[m_Level];
    public FarmLandUpgradeData CurrentUpgradeData => upgradeDataContainer[m_Level - 1];
    public FarmLandUpgradeData CurrentGrowthUpgradeData => upgradeDataContainer[m_GrowthLevel - 1];
    public int FruitLevel => m_Level;
    public int GrowthLevel => m_GrowthLevel;
    
    
    private List<FarmTile> m_FarmTiles;
    private Grid m_Grid;
    
    
    private void Start()
    {
        Build();
        if (unlockOnStart)
        {
            TryUnlock();
        }
    }


    public void OnSeedPlanted()
    {
        if (State == FarmLandState.Growing)
        {
            StartCoroutine(StartGrowing());
        }

        IEnumerator StartGrowing()
        {
            yield return null;
            
            foreach (var farmTile in m_FarmTiles)
            {
                farmTile.Harvestable.StartGrowing(CurrentGrowthUpgradeData.growthDuration);
            }
        }
    }

    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public bool TryUnlock()
    {
        if (Balance.TryRemove(CurrentUpgradeData.fruitUpgradeCost))
        {
            locks.SetActive(false);
            sign.Unlock(CurrentHarvestable.Data.icon);
            return true;
        }

        return false;
    }

    [Button(enabledMode: EButtonEnableMode.Playmode)]
    public bool TryUpgrade()
    {
        if (Balance.TryRemove(CurrentUpgradeData.fruitUpgradeCost))
        {
            m_Level++;
            m_Level = Mathf.Min(m_Level, harvestableContainer.Count);
            sign.Unlock(CurrentHarvestable.Data.icon);
            return true;
        }

        return false;
    }

    public bool TryUpgradeGrowth()
    {
        if (Balance.TryRemove(CurrentGrowthUpgradeData.growthReductionCost))
        {
            m_GrowthLevel++;
            m_GrowthLevel = Mathf.Min(m_GrowthLevel, upgradeDataContainer.Count);
            return true;
        }

        return false;
    }
    

    private void Build()
    {
        m_FarmTiles = new List<FarmTile>();
        m_Grid = new Grid(farmTileParent.position, x, y, size);

        foreach (var position in m_Grid)
        {
            var newFarmTile = Instantiate(farmTilePrefab, farmTileParent);
            newFarmTile.Position = position;
            newFarmTile.SetSize(size);
            newFarmTile.Inject(this);
            m_FarmTiles.Add(newFarmTile);
        }

        var boxColliderSize = boxCollider.size;
        boxColliderSize.x = size * x;
        boxColliderSize.z = size * y;
        boxCollider.size = boxColliderSize;

        var boxColliderCenter = boxCollider.center;
        boxColliderCenter.x = size * 0.5f * (x - 1);
        boxColliderCenter.z = size * 0.5f * (y - 1);
        boxCollider.center = boxColliderCenter;
    }
}
