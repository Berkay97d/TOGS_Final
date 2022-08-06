using System;
using EMRE.Scripts;
using LazyDoTween.Core;
using UnityEngine;

public class Juice : Item
{
    [SerializeField] private ItemData moneyBundle;
    
    [SerializeField] private DoLazyGroup doLazyGroup;


    private void Start()
    {
        PlayAnimation();
    }


    private void PlayAnimation()
    {
        doLazyGroup.Play();
    }
    
    
    public MoneyBundle TurnToMoneyBundle()
    {
        var money = Instantiate((MoneyBundle)moneyBundle.prefab);
        money.SetValue(Data.value);
        return money;
    }
}
