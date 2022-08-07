using Cinemachine;
using Helpers;
using UnityEngine;

public class CinemachineController : Scenegleton<CinemachineController>
{
    [SerializeField] private CinemachineBrain cinemachineBrain;
    
    [Header("Tutorial Virtual Cameras")]
    [SerializeField] private CinemachineVirtualCamera farmCam;
    [SerializeField] private CinemachineVirtualCamera juicerCam;
    [SerializeField] private CinemachineVirtualCamera sellerCam;
    [SerializeField] private CinemachineVirtualCamera gainCam;
    [SerializeField] private CinemachineVirtualCamera upgradeCam;

    [Header("Player Virtual Cameras:")]
    [SerializeField] private CinemachineVirtualCamera initialCam;
    [SerializeField] private CinemachineVirtualCamera standartCam;
    [SerializeField] private CinemachineVirtualCamera juicesSellingCam;
    [SerializeField] private CinemachineVirtualCamera moneyMakingCam;
    private void Start()
    {
        // InitialPriority();
        
        ClearTutorialCams();
        ClearPlayerCams();

        InitialPriority();
    }

    public static void Tutorial()
    {
        
    }

    public static void InitialPriority()
    {
        Instance.initialCam.Priority = 3;
        Instance.standartCam.Priority = 0;
        Instance.juicesSellingCam.Priority = 0;
        Instance.moneyMakingCam.Priority = 0;
    }

    public static void StandartPriority()
    {
        Instance.initialCam.Priority = 0;
        Instance.standartCam.Priority = 3;
        Instance.juicesSellingCam.Priority = 0;
        Instance.moneyMakingCam.Priority = 0;
    }

    public static void JuicesSelling()
    {
        Instance.initialCam.Priority = 0;
        Instance.standartCam.Priority = 0;
        Instance.juicesSellingCam.Priority = 3;
        Instance.moneyMakingCam.Priority = 0;
    }

    public static void MoneyMaking()
    {
        Instance.initialCam.Priority = 0;
        Instance.standartCam.Priority = 0;
        Instance.juicesSellingCam.Priority = 0;
        Instance.moneyMakingCam.Priority = 3;
    }
    
    

    private void ClearPlayerCams()
    {
        initialCam.Priority = 0;
        standartCam.Priority = 0;
        juicesSellingCam.Priority = 0;
        moneyMakingCam.Priority = 0;
    }

    private void ClearTutorialCams()
    {
        farmCam.Priority = 0;
        juicerCam.Priority = 0;
        sellerCam.Priority = 0;
        gainCam.Priority = 0;
        upgradeCam.Priority = 0;
    }
}
