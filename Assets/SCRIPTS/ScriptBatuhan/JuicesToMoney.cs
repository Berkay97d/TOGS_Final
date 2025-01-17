using System.Collections;
using System.Collections.Generic;
using EMRE.Scripts;
using UnityEngine;

public class JuicesToMoney : MonoBehaviour
{
    [SerializeField] private Transform moneyOutPoint, moneysParent;

    private static readonly WaitForSecondsRealtime MoneyInterval = new WaitForSecondsRealtime(0.1f);

    [SerializeField] private GameObject moneyBundlePrefab;
    public void MoneyMaker(Juice juice)
    {
        var money = juice.TurnToMoneyBundle();

        money.transform.position = moneyOutPoint.position;
        money.transform.SetParent(moneysParent);

        var topForce = moneyOutPoint.forward * 15f;
        var leftRightForce = moneyOutPoint.right*Random.Range(-3,3);
        money.GetComponent<Rigidbody>().AddForce(topForce+leftRightForce, ForceMode.Impulse);
        
        juice.Destroy();
    }
}
