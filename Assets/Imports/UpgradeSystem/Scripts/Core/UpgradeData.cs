using System;
using UnityEngine;

namespace UpgradeSystem.Core
{
    [Serializable]
    public struct UpgradeData<TCost, TValue>
    {
        public string saveKey;
        public int startLevel;
        public int endLevel;
        public TCost startCost;
        public TCost endCost;
        public AnimationCurve costCurve;
        public TValue startValue;
        public TValue endValue;
        public AnimationCurve valueCurve;
    }
}