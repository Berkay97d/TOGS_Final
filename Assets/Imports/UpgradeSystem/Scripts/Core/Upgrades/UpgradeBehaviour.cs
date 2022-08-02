using UnityEngine;
using UnityEngine.Events;

namespace UpgradeSystem.Core
{
    public abstract class UpgradeBehaviour<TCost, TValue> : MonoBehaviour
    {
        [SerializeField] private UpgradeData<TCost, TValue> data;


        public UnityEvent<UpgradeResponse<TCost, TValue>> 
            onLoad, 
            onUpgraded;


        public UpgradeData<TCost, TValue> Data => data;
        

        protected abstract BaseUpgradeCalculator<TCost, TValue> Calculator { get; set; }
        protected abstract bool CanAfford { get; }
        protected abstract bool CanAffordNext { get; }


        private UpgradeResponse<TCost, TValue> Response
        {
            get => new(CurrentCost, NextCost, CurrentValue, NextValue, CurrentLevel, IsMaxed, CanAffordNext);
        }

        private TCost CurrentCost => Calculator.CurrentCost;
        private TCost NextCost => Calculator.NextCost;

        private TValue CurrentValue => Calculator.CurrentValue;
        private TValue NextValue => Calculator.NextValue;

        private int CurrentLevel => Calculator.Level;
        private bool IsMaxed => Calculator.IsMaxed;


        protected abstract void PayCost(TCost cost);
        

        protected virtual void Start()
        {
            onLoad?.Invoke(Response);
        }

        public void Initialize(BaseUpgradeCalculator<TCost, TValue> calculator)
        {
            calculator.Inject(this);
            Calculator = calculator;
        }

        public void TryUpgrade()
        {
            if (!CanAfford) return;
            
            if (IsMaxed) return;
            
            Upgrade();
        }


        private void Upgrade()
        {
            var response = Response;
            Calculator.IncrementLevel();
            response.isMaxed = IsMaxed;
            PayCost(response.currentCost);
            onUpgraded?.Invoke(response);
        }
    }
}