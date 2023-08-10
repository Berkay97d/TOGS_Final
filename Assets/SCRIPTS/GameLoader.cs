using DG.Tweening;
using NaughtyAttributes;
using SupersonicWisdomSDK;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField, Scene] private int game;

    
    void Awake()
    {
        // Subscribe
        SupersonicWisdom.Api.AddOnReadyListener(OnSupersonicWisdomReady);
        // Then initialize
        SupersonicWisdom.Api.Initialize();
    }

    void OnSupersonicWisdomReady()
    {
        fill.DOFillAmount(1f, 3f)
            .From(0f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(game);
            });
    }
    
}
