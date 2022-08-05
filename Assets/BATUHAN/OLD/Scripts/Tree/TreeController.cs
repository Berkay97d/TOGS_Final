using UnityEngine;
using DG.Tweening;

public class TreeController : MonoBehaviour
{
    [SerializeField] private GameObject stump;
    public int treeHealth;
    [HideInInspector] public int treeInitHealth;

    private void Start()
    {
        treeInitHealth = treeHealth;
    }

    public void TakeDamage(int damage)
    {
        if(treeHealth == treeInitHealth)
            DestroyTreeTop();
        else
            DowngradeTree(damage);

        SpawnStump();
        treeHealth -= damage;
    }
    private void DestroyTreeTop()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
    private void DowngradeTree(float downAmount)
    {
        transform.position += Vector3.down*downAmount;
    }

    private float stumpOffset=0f;
    private void SpawnStump()
    {
        var stumpPosition = new Vector3(transform.position.x, 0.5f, transform.position.z); 
        var spawnedStump = Instantiate(stump, stumpPosition, stump.transform.rotation);
        spawnedStump.transform.DOMove(stumpPosition + Vector3.right + Vector3.forward * stumpOffset, 0.5f).SetEase(Ease.OutElastic)
            .OnComplete(() =>
            {
                spawnedStump.transform.DOMoveY(0.65f, 0.5f)
                    .SetEase(Ease.InElastic)
                    .SetLoops(-1, LoopType.Yoyo);
            });
        stumpOffset += 0.5f;
    }
}
