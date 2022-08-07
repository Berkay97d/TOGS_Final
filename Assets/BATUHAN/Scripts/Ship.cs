using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LazyDoTween.Core;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private DoLazyRotate _doLazyRotate;
    private DoLazyToggleMove _doLazyToggleMove;
    public Transform juicesStackPoint;

    private bool sellAnimationIsStarted = false;
    private int counter = 1;
    private void Start()
    {
        _doLazyRotate = GetComponent<DoLazyRotate>();
        _doLazyToggleMove = GetComponent<DoLazyToggleMove>();
        
        _doLazyRotate.Play();
    }

    public void GoSellJuicesShipAnimation()
    {
        if (!sellAnimationIsStarted)
        {
            _doLazyToggleMove.Enable();
            CinemachineController.MoneyMaking();
            counter++;
            Debug.Log("COUNTER!: " + counter);
            
            sellAnimationIsStarted = true;
        }
    }
    
    public void ComeSellJuicesShipAnimation()
    {
        for (int i = 0; i < juicesStackPoint.childCount; i++)
            Destroy(juicesStackPoint.GetChild(i).gameObject);
        
        _doLazyToggleMove.Disable();
        sellAnimationIsStarted = false;
    }
}
