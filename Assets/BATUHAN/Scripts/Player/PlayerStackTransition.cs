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
    [Range(0.1f,1f)][SerializeField] private float bagItemOffsetYAmount;

    [Header("JUICER")] 
    [SerializeField] private Juicer _juicer;
    [SerializeField]  private Transform juicerTank;
    [Range(1,10)][SerializeField] private float itemToJuicerSpeed;
    [Range(0.1f,3f)][SerializeField] private float itemToJuicerDelay;

    [Header("SHIP")] 
    [SerializeField] private Ship _ship;
    [Range(1,10)][SerializeField] private float itemToShipSpeed;
    [Range(0.1f,3f)][SerializeField] private float itemToShipDelay;

    [SerializeField] private int bottlesPerLine;

    private IEnumerator juicerCoroutine, shipCoroutine;
    private bool isJuicerCoroutineStarted = false, isShipCoroutineStarted = false;
    
    private float bottleOffsetZ = 0f;
    private float bottleOffsetX = 0f;
    
    
    private void Start()
    {
        bagItemOffsetY = 0f;
        
        LoadPlayerBagFromInventory();

        isJuicerCoroutineStarted = false;
        isShipCoroutineStarted = false;
        bottleOffsetZ = 0f;
        bottleOffsetX = 0f;


        juicerCoroutine = MoveToJuicerTank(null);
        shipCoroutine = MoveToShip(null);

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
        
        // SortBagItems();
        isJuicerCoroutineStarted = false;
    }

    
    
    
    public void JuicesMovingToShip()
    {
        if (!Inventory.IsEmpty() && !isShipCoroutineStarted)
        {
            var juices = new List<Juice>();
            for (int i = 0; i < playerBag.childCount; i++)
            {
                if (playerBag.GetChild(i).TryGetComponent(out Juice juice))
                {
                    juices.Add(juice);
                }
            }

            shipCoroutine = MoveToShip(juices);
            StartCoroutine(shipCoroutine);
        }
    }

    public void StopJuicesMovingToShip()
    {
        StopCoroutine(shipCoroutine);
        isShipCoroutineStarted = false;
    }

    IEnumerator MoveToShip(List<Juice> juices)
    {
        isShipCoroutineStarted = true;
        var itemsLength = juices.Count;
        while (itemsLength > 0)
        {
            if (itemsLength >= bottlesPerLine && itemsLength % bottlesPerLine == 0)
            {
                bottleOffsetX += 0.5f;
                bottleOffsetZ = 0f;
            }
            
            var juice = juices[--itemsLength];
            
            if (!juice) continue;
            
            juice.transform.SetParent(_ship.juicesStackPoint);
            
            var itemJuicesStackPointPosition = new Vector3(bottleOffsetX, 0, bottleOffsetZ);
            juice.transform.DOLocalRotate(Vector3.zero, 3f / itemToShipSpeed);
            juice.transform.DOLocalMove(itemJuicesStackPointPosition, 3f / itemToShipSpeed).SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {

                    if (Inventory.TryRemoveItem(juice))
                    {
                        _ship.GetComponent<JuicesToMoney>().MoneyMaker(juice);
                    }

                });
            bagItemOffsetY -= bagItemOffsetYAmount;
            bottleOffsetZ += 0.5f;
            
            yield return new WaitForSeconds(itemToShipDelay);
        }

        _ship.GoSellJuicesShipAnimation();
        bottleOffsetZ = 0f;
        bottleOffsetX = 0f;

        // SortBagItems();
        isShipCoroutineStarted = false;
    }

    public void SortBagItems()
    {
        bagItemOffsetY = 0f;
        for (int i = 0; i < playerBag.childCount; i++)
        {
            var itemBagPosition = new Vector3(0, bagItemOffsetY, 0);
            playerBag.GetChild(i).DOLocalMove(itemBagPosition, 3f / itemToBagSpeed).SetEase(Ease.OutCubic);
            playerBag.GetChild(i).DOLocalRotate(Vector3.zero, 3f / itemToBagSpeed);

            bagItemOffsetY += bagItemOffsetYAmount;
        }
    }
}
