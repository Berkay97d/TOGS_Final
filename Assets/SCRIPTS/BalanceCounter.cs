using System;
using Helpers;
using IdleCashSystem.Core;
using TMPro;
using UnityEngine;

public class BalanceCounter : Scenegleton<BalanceCounter>
{
    [SerializeField] private TMP_Text balanceField;


    public static Vector3 Position => Instance.transform.position;
    
    
    private void Start()
    {
        UpdateBalance(Balance.Amount);
    }
    
    private void OnEnable()
    {
        Balance.OnChanged += UpdateBalance;
    }

    private void OnDisable()
    {
        Balance.OnChanged -= UpdateBalance;
    }


    private void UpdateBalance(IdleCash amount)
    {
        balanceField.text = amount.ToString();
    }
}
