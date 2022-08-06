using System;
using EMRE.Scripts;
using LazyDoTween.Core;
using UnityEngine;

public class Juice : Item
{
    [SerializeField] private ItemData moneyBundle;
    [SerializeField] private DoLazyGroup doLazyGroup;


    protected override void Start()
    {
        base.Start();
        PlayAnimation();
    }

    
    public MoneyBundle TurnToMoneyBundle()
    {
        var money = Instantiate((MoneyBundle)moneyBundle.prefab);
        money.SetValue(Data.value);
        return money;
    }
    

    private void PlayAnimation()
    {
        doLazyGroup.Play();
    }
}
