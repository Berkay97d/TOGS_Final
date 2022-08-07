using DummyAds.Core;
using EMRE.Scripts;
using LazyDoTween.Core;
using NaughtyAttributes;
using UnityEngine;

public class MoneyCrate : MonoBehaviour
{
    
    [SerializeField] private DoLazyToggleGroup moneyMultiplierLazyToggleGroup;
    public Transform moneysParent;
    
    [Button()]
    public void EnableMoneyMultiplierButton()
    {
        moneyMultiplierLazyToggleGroup.Enable();
    }
    [Button()]
    public void DisableMoneyMultiplierButton()
    {
        moneyMultiplierLazyToggleGroup.Disable();
    }
    
    
    public void MoneyMultiplier()
    {
        if (moneysParent.childCount > 0)
        {
            var rewardedAd = DummyAdsManager
                .BuildRewardedDummyAd(DummyAdOrientation.Portrait);

            rewardedAd.SetDuration(5);
            rewardedAd.OnRewardGranted += AdsWatched;
        }
    }

    public void AdsWatched()
    {
        var currentMoneyBundleCount = moneysParent.childCount;
        for (int i = 0; i < currentMoneyBundleCount; i++)
        {
            var spawnedMoney = Instantiate(moneysParent.GetChild(i).gameObject, moneysParent);
            spawnedMoney.GetComponent<MoneyBundle>().SetValue(moneysParent.GetChild(i).GetComponent<MoneyBundle>().Value);
            spawnedMoney.transform.position += Vector3.up;
            spawnedMoney.transform.localScale = Vector3.one;
        }

        DisableMoneyMultiplierButton();
    }
}
