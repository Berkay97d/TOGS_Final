using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Helpers;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CinemachineController : Scenegleton<CinemachineController>
{
    [SerializeField] private CinemachineBrain cinemachineBrain;

    [Header("Virtual Cameras:")]
    [SerializeField] private CinemachineVirtualCamera initialCam;
    [SerializeField] private CinemachineVirtualCamera standartCam;
    [SerializeField] private CinemachineVirtualCamera juicesSellingCam;
    [SerializeField] private CinemachineVirtualCamera moneyMakingCam;
    private void Start()
    {
        InitialPriority();
    }

    public static void InitialPriority()
    {
        Instance.initialCam.Priority = 3;
        Instance.standartCam.Priority = 2;
        Instance.juicesSellingCam.Priority = 1;
        Instance.moneyMakingCam.Priority = 0;
    }

    public static void StandartPriority()
    {
        Instance.initialCam.Priority = 0;
        Instance.standartCam.Priority = 3;
        Instance.juicesSellingCam.Priority = 1;
        Instance.moneyMakingCam.Priority = 2;
    }

    public static void JuicesSelling()
    {
        Instance.initialCam.Priority = 0;
        Instance.standartCam.Priority = 1;
        Instance.juicesSellingCam.Priority = 3;
        Instance.moneyMakingCam.Priority = 2;
    }

    public static void MoneyMaking()
    {
        Instance.initialCam.Priority = 0;
        Instance.standartCam.Priority = 1;
        Instance.juicesSellingCam.Priority = 2;
        Instance.moneyMakingCam.Priority = 3;
    }
}
