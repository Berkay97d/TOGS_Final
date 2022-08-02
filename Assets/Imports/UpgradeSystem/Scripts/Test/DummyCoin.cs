using UnityEngine;
using UnityEngine.Events;

namespace UpgradeSystem.Scripts.Test
{
    public static class DummyCoin
    {
        private const string DummyCoinKey = "Dummy_Coin";


        public static UnityAction<int> OnChanged;


        public static int Amount
        {
            get => PlayerPrefs.GetInt(DummyCoinKey, 0);
            private set => PlayerPrefs.SetInt(DummyCoinKey, value);
        }


        public static bool HasEnough(int amount)
        {
            return amount <= Amount;
        }

        public static bool TryRemove(int amount)
        {
            if (!HasEnough(amount)) return false;

            Amount -= amount;
            OnChanged?.Invoke(Amount);

            return true;
        }

        public static void Add(int amount)
        {
            Amount += amount;
            OnChanged?.Invoke(Amount);
        }
    }
}
