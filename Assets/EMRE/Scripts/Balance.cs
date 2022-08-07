using Helpers;
using IdleCashSystem.Core;
using UnityEngine.Events;

public static class Balance
{
    private const string BalanceKey = "Balance";


    public static UnityAction<IdleCash> OnChanged;


    public static IdleCash Amount
    {
        get => JsonPrefs.Load(BalanceKey, IdleCash.One * 12000);
        private set => JsonPrefs.Save(BalanceKey, value);
    }


    public static bool HasEnough(IdleCash amount)
    {
        return amount <= Amount;
    }

    public static bool TryRemove(IdleCash amount)
    {
        if (!HasEnough(amount)) return false;

        Amount -= amount;
        OnChanged?.Invoke(Amount);

        return true;
    }

    public static void Add(IdleCash amount)
    {
        Amount += amount;
        OnChanged?.Invoke(Amount);
    }
}
