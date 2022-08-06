using System.Collections;
using System.Collections.Generic;
using EMRE.Scripts;
using UnityEngine;

public class JuicesToMoney : MonoBehaviour
{
    [SerializeField] private Transform moneyOutPoint;

    private static readonly WaitForSecondsRealtime MoneyInterval = new WaitForSecondsRealtime(0.1f);
    
    
    public void MoneyMaker(Juice juice)
    {
        var money = juice.TurnToMoneyBundle();

        money.transform.position = moneyOutPoint.position;

        var force = moneyOutPoint.forward * 10f;
        juice.Throw(force);
        
        juice.Destroy();
    }
}
