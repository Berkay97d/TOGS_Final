using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EMRE.Scripts;
using IdleCashSystem.Core;
using LazyDoTween.Core;
using UnityEngine;
using UpgradeSystem.Core;

public class PlayerStackTransition : MonoBehaviour
{
    [Header("PLAYER BAG")]
    [SerializeField] private Transform playerBag;
    [Range(1, 10)] [SerializeField] private float itemToBagSpeed;
    private float bagItemOffsetY = 0;
    [Range(0.1f,1f)][SerializeField] private float bagItemOffsetYAmount;

    [Header("JUICER")] 
    [SerializeField] private Juicer _juicer;
    [SerializeField] private Transform juicerTank;
    [SerializeField] private Transform[] juicerInPoints;
    [Range(1,10)][SerializeField] private float itemToJuicerSpeed;
    [Range(0.1f,3f)][SerializeField] private float itemToJuicerDelay;

    [Header("SHIP")] 
    [SerializeField] private Ship _ship;
    [Range(1,10)][SerializeField] private float itemToShipSpeed;
    [Range(0.1f,3f)][SerializeField] private float itemToShipDelay;

    [SerializeField] private int bottlesPerHorizontalLine, bottlesPerVerticalLine;
    private Vector3 bottleOffset;

    private IEnumerator juicerCoroutine, shipCoroutine;
    private bool isJuicerCoroutineStarted, isShipCoroutineStarted;
    
    public IdleCash BagSize { get; private set; }
    
    private void Start()
    {
        bagItemOffsetY = 0f;
        
        LoadPlayerBagFromInventory();

        isJuicerCoroutineStarted = false;
        isShipCoroutineStarted = false;
        bottleOffset = Vector3.zero;


        juicerCoroutine = MoveToJuicerTank(null);
        shipCoroutine = MoveToShip(null);


        BagSize = IdleCash.One * 100;
    }


    public void OnSizeLoaded(UpgradeResponse<IdleCash, IdleCash> response)
    {
        ChangeSize(response.currentValue);
    }
    
    public void OnSizeUpgraded(UpgradeResponse<IdleCash, IdleCash> response)
    {
        ChangeSize(response.nextValue);
    }

    private void ChangeSize(IdleCash newSize)
    {
        BagSize = newSize;
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
        
        CapacityDisplay.OnItemCountChanged(IdleCash.One * playerBag.childCount);
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
        
        CapacityDisplay.OnItemCountChanged(IdleCash.One * playerBag.childCount);
    }

    public void JuicerTankMoving()
    {
        if (!isJuicerCoroutineStarted)
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
            
            CapacityDisplay.OnItemCountChanged(IdleCash.One * playerBag.childCount);
            
            fruit.transform.SetParent(juicerTank, true);
            
            Sequence mySequence = DOTween.Sequence();

            for (int i = 0; i < juicerInPoints.Length; i++)
            {
                mySequence.Append(i == 0
                    ? fruit.transform.DOMove(juicerInPoints[i].position, 3f / itemToJuicerSpeed).SetEase(Ease.InSine)
                    : fruit.transform.DOMove(juicerInPoints[i].position, 1f / itemToJuicerSpeed).SetEase(Ease.OutCubic));
            }
                
            mySequence.Join(fruit.transform.DOLocalRotate(Vector3.zero, 3f / itemToJuicerSpeed));
            mySequence.OnComplete(() => {
                if (Inventory.TryRemoveItem(fruit)) _juicer.GetComponent<Juicer>().JuiceFruit(fruit);
            });
            
            bagItemOffsetY -= bagItemOffsetYAmount;
            
            yield return new WaitForSeconds(itemToJuicerDelay);
        }
        
        SortBagItems();
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

            Debug.Log("MONEY!");

            shipCoroutine = MoveToShip(juices);
            StartCoroutine(shipCoroutine);
        }
    }

    public void StopJuicesMovingToShip()
    {
        StopCoroutine(shipCoroutine);
        isShipCoroutineStarted = false;
    }

    private IEnumerator MoveToShip(List<Juice> juices)
    {
        isShipCoroutineStarted = true;
        var itemsLength = juices.Count;
        int bottleCounter = 0;
        while (itemsLength > 0)
        {
            var juice = juices[--itemsLength];
            if (!juice) continue;
            
            if (bottleCounter != 0 && bottleCounter % (bottlesPerHorizontalLine*bottlesPerVerticalLine) == 0)
            {
                bottleOffset = new Vector3(0, bottleOffset.y + 0.6f, 0);
            }
            else if (bottleCounter != 0 && bottleCounter % bottlesPerHorizontalLine == 0)
            {
                bottleOffset = new Vector3(bottleOffset.x + 0.6f, bottleOffset.y, 0);
            }

            
            juice.transform.SetParent(_ship.juicesStackPoint);
            CapacityDisplay.OnItemCountChanged(IdleCash.One * playerBag.childCount);
            
            var itemJuicesStackPointPosition = bottleOffset;
            juice.transform.DOLocalRotate(Vector3.zero, 3f / itemToShipSpeed);
            juice.transform.DOLocalMove(itemJuicesStackPointPosition, 3f / itemToShipSpeed).SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    if (Inventory.TryRemoveItem(juice))
                    {
                        Instantiate(juice, _ship.juicesStackPoint);
                        _ship.GetComponent<JuicesToMoney>().MoneyMaker(juice);
                    }
                });
            bottleCounter++;
            bagItemOffsetY -= bagItemOffsetYAmount;
            bottleOffset = new Vector3(bottleOffset.x, bottleOffset.y, bottleOffset.z + 0.6f);
            
            yield return new WaitForSeconds(itemToShipDelay);
        }

        _ship.GoSellJuicesShipAnimation();
        bottleOffset = new Vector3(0, bottleOffset.y, 0);

        SortBagItems();
        isShipCoroutineStarted = false;

        yield return new WaitForSeconds(2f);
        CinemachineController.StandartPriority();
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
