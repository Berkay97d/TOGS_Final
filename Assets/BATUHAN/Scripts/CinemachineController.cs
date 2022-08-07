using Cinemachine;
using Helpers;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CinemachineController : Scenegleton<CinemachineController>
{
    [SerializeField] private CinemachineBrain cinemachineBrain;
    
    [Header("Tutorial Virtual Cameras")]
    [SerializeField] private CinemachineVirtualCamera tutorialFarmCam;
    [SerializeField] private CinemachineVirtualCamera tutorialJuicerCam;
    [SerializeField] private CinemachineVirtualCamera tutorialSellerCam;
    [SerializeField] private CinemachineVirtualCamera tutorialGainCam;
    [SerializeField] private CinemachineVirtualCamera tutorialUpgradeCam;

    [Header("Player Virtual Cameras:")]
    [SerializeField] private CinemachineVirtualCamera initialCam;
    [SerializeField] private CinemachineVirtualCamera standartCam;
    [SerializeField] private CinemachineVirtualCamera juicesSellingCam;
    [SerializeField] private CinemachineVirtualCamera moneyMakingCam;
    private void Start()
    {
        ClearTutorialCams();
        ClearPlayerCams();

        InitialPriority();
    }

    public static void Tutorial()
    {
        TutorialFarmPriority();
        Instance.Invoke(nameof(TutorialJuicerPriority), 1f);

    }

    public static void InitialPriority()
    {
        ClearPlayerCams();
        Instance.initialCam.Priority = 1;
    }
    public static void StandartPriority()
    {
        ClearPlayerCams();
        Instance.standartCam.Priority = 1;
    }
    public static void JuicesSelling()
    {
        ClearPlayerCams();
        Instance.juicesSellingCam.Priority = 1;
    }
    public static void MoneyMaking()
    {
        ClearPlayerCams();
        Instance.moneyMakingCam.Priority = 1;
    }
    private static void ClearPlayerCams()
    {
        Instance.initialCam.Priority = 0;
        Instance.standartCam.Priority = 0;
        Instance.juicesSellingCam.Priority = 0;
        Instance.moneyMakingCam.Priority = 0;
    }

    
    public static void TutorialFarmPriority()
    {
        ClearTutorialCams();
        Instance.tutorialFarmCam.Priority = 1;
    }
    public static void TutorialJuicerPriority()
    {
        ClearTutorialCams();
        Instance.tutorialJuicerCam.Priority = 1;
    }
    public static void TutorialSellerPriority()
    {
        ClearTutorialCams();
        Instance.tutorialSellerCam.Priority = 1;
    }
    public static void TutorialGainPriority()
    {
        ClearTutorialCams();
        Instance.tutorialGainCam.Priority = 1;
    }
    public static void TutorialUpgradePriority()
    {
        ClearTutorialCams();
        Instance.tutorialUpgradeCam.Priority = 1;
    }
    private static void ClearTutorialCams()
    {
        Instance.tutorialFarmCam.Priority = 0;
        Instance.tutorialJuicerCam.Priority = 0;
        Instance.tutorialSellerCam.Priority = 0;
        Instance.tutorialGainCam.Priority = 0;
        Instance.tutorialUpgradeCam.Priority = 0;
    }
}
