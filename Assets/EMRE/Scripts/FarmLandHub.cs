using LazyDoTween.Core;
using NaughtyAttributes;
using UnityEngine;

public class FarmLandHub : MonoBehaviour
{
    [SerializeField] private FarmLand farmLand;
    [SerializeField] private DoLazyToggleGroup 
        upgradableLazyToggleGroup, 
        unlockableLazyToggleGroup;


    public bool IsLocked => farmLand.IsLocked;
    public FarmLand FarmLand => farmLand;
    

    [Button()]
    public void EnableUpgradable()
    {
        //upgradableButtons.SetActive(true);
        upgradableLazyToggleGroup.Enable();
    }
    [Button()]
    public void DisableUpgradable()
    {
        upgradableLazyToggleGroup.Disable();
    }


    [Button()]
    public void EnableUnlockable()
    {
        //unlockableButton.SetActive(true);
        unlockableLazyToggleGroup.Enable();
    }
    [Button()]
    public void DisableUnlockable()
    {
        //unlockableButton.SetActive(false);
        unlockableLazyToggleGroup.Disable();
    }

    public void Unlock()
    {
        if (farmLand.TryUnlock())
        {
            DisableUnlockable();
        }
    }
    
    public void Upgrade()
    {
        farmLand.TryUpgrade();
    }

    public void UpgradeGrowth()
    {
        farmLand.TryUpgradeGrowth();
    }
}
