using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Helpers;
using UnityEngine;

public class CinemachineController : Scenegleton<CinemachineController>
{
    [SerializeField] private CinemachineBrain cinemachineBrain;

    [Header("Virtual Cameras:")] [SerializeField]
    private CinemachineVirtualCamera standartCam;
    [SerializeField] private CinemachineVirtualCamera juicesSellingCam;
    private void Start()
    {
        InitialPriority();
    }

    public static void InitialPriority()
    {
        Instance.standartCam.Priority = 1;
        Instance.juicesSellingCam.Priority = 0;
    }

    public static void JuicesSelling()
    {
        Instance.standartCam.Priority = 0;
        Instance.juicesSellingCam.Priority = 1;
    }
}
