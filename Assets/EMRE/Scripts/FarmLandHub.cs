using LazyDoTween.Core;
using NaughtyAttributes;
using UnityEngine;

public class FarmLandHub : MonoBehaviour
{
    [SerializeField] private DoLazyToggleGroup upgradableLazyToggleGroup, unlockableLazyToggleGroup;
    [SerializeField] private GameObject upgradableButtons, unlockableButton;


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
}
