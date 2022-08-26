using DG.Tweening;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("Lightning Object:")]
    [SerializeField] private Material material;
    [SerializeField] private Color matInitialColor, matTargetColor;
    [SerializeField] private float lightningSpeed  = 1;
    [SerializeField] private Ease AnimationType = Ease.Linear;
    [SerializeField] private int loopCount = -1;
    [SerializeField] private LoopType loopType = LoopType.Yoyo;
    private void Start()
    {
        if (material == null) material = GetComponent<Material>();
        
        material.color = matInitialColor;
        material.DOColor(matTargetColor, lightningSpeed)
            .SetEase(Ease.Flash)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
