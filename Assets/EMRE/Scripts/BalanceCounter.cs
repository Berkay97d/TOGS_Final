using System;
using IdleCashSystem.Core;
using TMPro;
using UnityEngine;

public class BalanceCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceField;


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
