using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EMRE.Scripts;
using IdleCashSystem.Core;
using LazyDoTween.Core;
using UnityEngine;

public class PlayerStackTransition : MonoBehaviour
{
    [Header("PLAYER BAG")]
    public IdleCash bagSize;
    [SerializeField] private Transform playerBag;
    [Range(1, 10)] [SerializeField] private float itemToBagSpeed;
    private float bagItemOffsetY = 0;
    [Range(0.1f,1f)][SerializeField] private float bagItemOffsetYAmount = 0f;

    [Header("JUICER")] 
    [SerializeField] private Juicer _juicer;
    [SerializeField]  private Transform juicerTank;
    [Range(1,10)][SerializeField] private float itemToJuicerSpeed;
    [Range(0.1f,3f)][SerializeField] private float itemToJuicerDelay;


    private IEnumerator juicerCoroutine;
    private bool isJuicerCoroutineStarted = false;
    private void Start()
    {
        LoadPlayerBagFromInventory();
        
        
        
        juicerCoroutine = MoveToJuicerTank(null);
    }

    private void LoadPlayerBagFromInventory()
    {
        foreach (var elem in Inventory.Stock)
        {
            for (IdleCash i = IdleCash.Zero; i < elem.value; i += IdleCash.One)
            {
                var newItem = Instantiate(elem.key.prefab, transform);
                newItem.transform.localPosition = new Vector3(0, bagItemOffsetY, 0);
                
                CollectItem(newItem.transform);
            }
        }
    }


    public void CollectItem(Transform item)
    {
        Destroy(item.GetComponent<Rigidbody>());
        Destroy(item.GetComponent<SphereCollider>());

        item.SetParent(playerBag);
        
        var itemBagPosition = new Vector3(0, bagItemOffsetY, 0);
        item.DOLocalMove(itemBagPosition, 3f / itemToBagSpeed).SetEase(Ease.OutCubic);
        item.DOLocalRotate(Vector3.zero, 3f / itemToBagSpeed);

        bagItemOffsetY += bagItemOffsetYAmount;
    }

    public void JuicerTankMoving()
    {
        if (!Inventory.IsEmpty() && !isJuicerCoroutineStarted)
        {
            var fruits = new List<Fruit>();
            for (int i = 0; i < playerBag.childCount; i++)
            {
                if (playerBag.GetChild(i).TryGetComponent(out Fruit fruit))
                {
                    fruits.Add(fruit);
                }
            }

            juicerCoroutine = MoveToJuicerTank(fruits);
            StartCoroutine(juicerCoroutine);
        }
    }

    public void StopJuicerTankMoving()
    {
        StopCoroutine(juicerCoroutine);
        isJuicerCoroutineStarted = false;
    }

    IEnumerator MoveToJuicerTank(List<Fruit> fruits)
    {
        isJuicerCoroutineStarted = true;
        var itemsLength = fruits.Count;
        while (itemsLength > 0)
        {
            var fruit = fruits[--itemsLength];
            
            if (!fruit) continue;
            
            fruit.transform.SetParent(juicerTank, true);

            var itemJuicerTankPosition = new Vector3(0, 0, 0);
            fruit.transform.DOLocalRotate(Vector3.zero, 3f / itemToJuicerSpeed);
            fruit.transform.DOLocalMove(itemJuicerTankPosition, 3f / itemToJuicerSpeed).SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {

                    if (Inventory.TryRemoveItem(fruit))
                    {
                        _juicer.GetComponent<Juicer>().JuiceFruit(fruit);
                    }

                });
            bagItemOffsetY -= bagItemOffsetYAmount;
            
            yield return new WaitForSeconds(itemToJuicerDelay);
        }
        isJuicerCoroutineStarted = false;
    }
}
