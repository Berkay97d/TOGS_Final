using LazyDoTween.Core;
using NaughtyAttributes;
using UnityEngine;

public class FarmLandHub : MonoBehaviour
{
    [SerializeField] private FarmLand farmLand;
    [SerializeField] private DoLazyToggleGroup 
        upgradableLazyToggleGroup, 
        unlockableLazyToggleGroup,
        workerUnlockLazyToggleGroup;


    public bool IsLocked => farmLand.IsLocked;
    public bool HasWorker => farmLand.HasWorker;
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

    [Button()]
    public void EnableWorkerUnlock()
    {
        workerUnlockLazyToggleGroup.Enable();
    }
    
    [Button()]
    public void DisableWorkerUnlock()
    {
        workerUnlockLazyToggleGroup.Disable();
    }

    public void Unlock()
    {
        if (farmLand.TryUnlock())
        {
            DisableUnlockable();
        }
    }

    public void UnlockWorker()
    {
        if (farmLand.TryUnlockWorker())
        {
            DisableWorkerUnlock();
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
