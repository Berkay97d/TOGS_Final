using System;
using System.Collections.Generic;
using Helpers;
using IdleCashSystem.Core;
using UnityEngine.Events;

public static class Inventory
{
    private const string InventoryStockKey = "Inventory_Stock";


    public static UnityAction<List<MyKeyValuePair<ItemData, IdleCash>>> OnChanged;


    public static List<MyKeyValuePair<ItemData, IdleCash>> Stock
    {
        get
        {
            var array = JsonPrefs.LoadArray(InventoryStockKey, new MyKeyValuePair<ItemData, IdleCash>[]{});
            return new List<MyKeyValuePair<ItemData, IdleCash>>(array);
        }

        set => JsonPrefs.SaveArray(InventoryStockKey, value.ToArray());
    }


    public static void StackItem(ItemData item, IdleCash amount)
    {
        var stock = Stock;

        var hasItem = false;
        MyKeyValuePair<ItemData, IdleCash> itemPair = null;
        foreach (var pair in stock)
        {
            if (pair.key == item)
            {
                hasItem = true;
                itemPair = pair;
                break;
            }
        }

        if (hasItem)
        {
            itemPair.value += amount;
            Stock = stock;
            OnChanged?.Invoke(stock);
            return;
        }
        
        stock.Add(new MyKeyValuePair<ItemData, IdleCash>()
        {
            key = item,
            value = amount
        });
        Stock = stock;
        OnChanged?.Invoke(stock);
    }

    public static bool TryPopItem(out ItemData item)
    {
        var stock = Stock;

        if (stock.Count <= 0)
        {
            item = null;
            return false;
        }

        var lastIndex = stock.Count - 1;
        var last = stock[lastIndex];
        
        last.value -= IdleCash.One;

        if (last.value <= IdleCash.Zero)
        {
            stock.RemoveAt(lastIndex);
        }
        
        Stock = stock;
        OnChanged?.Invoke(stock);
        item = last.key;

        return true;
    }
    
    
    [Serializable]
    public class MyKeyValuePair<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }
}
