using DummyAds.Core;
using Helpers;
using UnityEngine;

public class InGameAds : Scenegleton<InGameAds>
{
    private const string BridgeAdKey = "Bridge_Ad";
    private const int BridgeAdRate = 3;


    private static int BridgePassCount
    {
        get => PlayerPrefs.GetInt(BridgeAdKey, 0);
        set => PlayerPrefs.SetInt(BridgeAdKey, value);
    }


    public static void OnPassedBridge()
    {
        BridgePassCount += 1;

        DummyAdsManager
            .BuildRewardedDummyAd(DummyAdOrientation.Portrait)
            .SetDuration(5);
    }
}
