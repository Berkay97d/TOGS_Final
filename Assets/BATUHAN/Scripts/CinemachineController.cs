using Cinemachine;
using Helpers;
using UnityEngine;

public class CinemachineController : Scenegleton<CinemachineController>
{
    private const string TutorialKey = "Tutorial";
    
    
    [SerializeField] private CinemachineBrain cinemachineBrain;

    public static bool isTutorialCamActive;
    
    [Header("Tutorial Virtual Cameras")]
    [SerializeField] private CinemachineVirtualCamera farmCam;
    [SerializeField] private CinemachineVirtualCamera juicerCam;
    [SerializeField] private CinemachineVirtualCamera sellerCam;
    [SerializeField] private CinemachineVirtualCamera gainCam;
    [SerializeField] private CinemachineVirtualCamera upgradeCam;
    [SerializeField] private float tutorialCamDuration = 3;

    [Header("Player Virtual Cameras:")]
    [SerializeField] private CinemachineVirtualCamera initialCam;
    [SerializeField] private CinemachineVirtualCamera standartCam;
    [SerializeField] private CinemachineVirtualCamera juicesSellingCam;
    [SerializeField] private CinemachineVirtualCamera moneyMakingCam;


    private static bool IsTutorialComplete
    {
        get => PlayerPrefs.GetInt(TutorialKey, 0) == 1;
        set => PlayerPrefs.SetInt(TutorialKey, value ? 1 : 0);
    }
    
    
    private void Start()
    {
        ClearTutorialCams();
        ClearPlayerCams();

        if (IsTutorialComplete)
        {
            InitialPriority();
        }
        else
        {
            StartTutorialCams();
        }
    }
    
    public void InitialPriority()
    {
        IsTutorialComplete = true;
        isTutorialCamActive = false;
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

    
    

    public void StartTutorialCams()
    {
        isTutorialCamActive = true;
        
        TutorialFarmPriority();
        Instance.Invoke(nameof(TutorialJuicerPriority), tutorialCamDuration);
    }
    public void TutorialFarmPriority()
    {
        ClearTutorialCams();
        Instance.farmCam.Priority = 1;
        
        Instance.Invoke(nameof(TutorialJuicerPriority), tutorialCamDuration);
    }
    public void TutorialJuicerPriority()
    {
        ClearTutorialCams();
        Instance.juicerCam.Priority = 1;
        
        Instance.Invoke(nameof(TutorialSellerPriority), tutorialCamDuration);
    }
    public void TutorialSellerPriority()
    {
        ClearTutorialCams();
        Instance.sellerCam.Priority = 1;
        
        Instance.Invoke(nameof(TutorialGainPriority), tutorialCamDuration);
    }
    public void TutorialGainPriority()
    {
        ClearTutorialCams();
        Instance.gainCam.Priority = 1;
        
        Instance.Invoke(nameof(TutorialUpgradePriority), tutorialCamDuration);
    }
    public void TutorialUpgradePriority()
    {
        ClearTutorialCams();
        upgradeCam.Priority = 1;

        Instance.Invoke(nameof(InitialPriority), tutorialCamDuration);
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
