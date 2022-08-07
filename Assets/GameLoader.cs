using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField, Scene] private int game;


    private void Start()
    {
        fill.DOFillAmount(1f, 3f)
            .From(0f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(game);
            });
    }
}
