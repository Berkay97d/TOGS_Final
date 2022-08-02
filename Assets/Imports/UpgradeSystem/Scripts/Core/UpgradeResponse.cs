using System;

namespace UpgradeSystem.Core
{
    [Serializable]
    public struct UpgradeResponse<TCost, TValue>
    {
        public TCost currentCost;
        public TCost nextCost;
        public TValue currentValue;
        public TValue nextValue;
        public int currentLevel;
        public bool isMaxed;
        public bool canAffordNext;

        public int NextLevel => currentLevel + 1;


        public UpgradeResponse(
            TCost currentCost, 
            TCost nextCost, 
            TValue currentValue, 
            TValue nextValue, 
            int currentLevel, 
            bool isMaxed,
            bool canAffordNext)
        {
            this.currentCost = currentCost;
            this.nextCost = nextCost;
            this.currentValue = currentValue;
            this.nextValue = nextValue;
            this.currentLevel = currentLevel;
            this.isMaxed = isMaxed;
            this.canAffordNext = canAffordNext;
        }

        public override string ToString()
        {
            return $"Level: {currentLevel} -> Level {NextLevel} | Cost: {currentCost} -> {nextCost} | Value: {currentValue} -> {nextValue} | Is Maxed: {isMaxed} | Can Afford Next: {canAffordNext}";
        }
    }
}