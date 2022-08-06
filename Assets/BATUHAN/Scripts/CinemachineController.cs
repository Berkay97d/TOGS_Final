using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cinemachineBrain;

    [Header("Virtual Cameras:")] [SerializeField]
    private CinemachineVirtualCamera standartCam;
    [SerializeField] private CinemachineVirtualCamera juicesSellingCam;
    private void Start()
    {
        InitialPriority();
    }

    public void InitialPriority()
    {
        standartCam.Priority = 1;
        juicesSellingCam.Priority = 0;
    }

    public void JuicesSelling()
    {
        standartCam.Priority = 0;
        juicesSellingCam.Priority = 1;
    }
}
