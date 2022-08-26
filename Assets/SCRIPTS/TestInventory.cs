using System;
using System.Collections.Generic;
using IdleCashSystem.Core;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    [SerializeField] private ItemData item;
    [SerializeField] private IdleCash amount;


    private void Start()
    {
        PrintInventory(Inventory.Stock);
    }


    private void OnEnable()
    {
        Inventory.OnChanged += PrintInventory;
    }
    
    private void OnDisable()
    {
        Inventory.OnChanged -= PrintInventory;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Inventory.StackItem(item, amount);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Inventory.TryPopItem(out var item))
            {
                Debug.Log(item.name);
            }
        }
    }

    private void PrintInventory(List<Inventory.MyKeyValuePair<ItemData, IdleCash>> inv)
    {
        foreach (var pair in inv)
        {
            Debug.Log($"{pair.key.name} : {pair.value}");
        }
    }
}
