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

    private void Start()
    {
        _doLazyRotate = GetComponent<DoLazyRotate>();
        _doLazyToggleMove = GetComponent<DoLazyToggleMove>();
        
        _doLazyRotate.Play();
    }

    public void GoSellJuicesShipAnimation()
    {
        CinemachineController.InitialPriority();
        _doLazyToggleMove.Enable();
    }
    public void ComeSellJuicesShipAnimation()
    {
        for (int i = 0; i < juicesStackPoint.childCount; i++)
            Destroy(juicesStackPoint.GetChild(i).gameObject);
        
        _doLazyToggleMove.Disable();
    }
}
