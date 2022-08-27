using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FruitJuiceSell_TEST : MonoBehaviour
{
    [SerializeField] private GameObject juice;
    [SerializeField] private int juiceCount;


    [SerializeField] private Transform player;
    private float bagItemOffsetY = 0;
    [SerializeField] private float bagItemOffsetYAmount;
    
    [SerializeField] private Transform juicesStackPoint, juicesSellPoint;

    private List<GameObject> juices = new List<GameObject>();
    [SerializeField] private Ease easeType;
    [SerializeField] private int itemToShipSpeed;
    [SerializeField] private float itemToShipDelay;
    
    private IEnumerator shipCoroutine;
    private bool isShipCoroutineStarted = false;

    [SerializeField] private int bottlesPerHorizontalLine, bottlesPerVerticalLine;
    private Vector3 bottleOffset;
    
    private void Start()
    {
        bagItemOffsetY = 0;
        isShipCoroutineStarted = false;
        bottleOffset = Vector3.zero;

        shipCoroutine = MoveToShip(null);
        
        for (int i = 0; i < juiceCount; i++)
        {
            var newJuice = Instantiate(juice, player);
            juices.Add(newJuice);
            
            newJuice.transform.localPosition += Vector3.up*bagItemOffsetY;
            bagItemOffsetY += bagItemOffsetYAmount;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JuicesMovingToShip();
        }
    }
    
    public void JuicesMovingToShip()
    {
        shipCoroutine = MoveToShip(juices);
        StartCoroutine(shipCoroutine);
    }

    public void StopJuicesMovingToShip()
    {
        StopCoroutine(shipCoroutine);
        isShipCoroutineStarted = false;
    }
    
    private IEnumerator MoveToShip(List<GameObject> juices)
    {
        isShipCoroutineStarted = true;
        var itemsLength = juices.Count;
        int bottleCounter = 0;
        while (itemsLength > 0)
        {
            var juice = juices[--itemsLength];
            if (!juice) continue;
            
            if (bottleCounter % (bottlesPerHorizontalLine*bottlesPerVerticalLine) == 0)
            {
                 bottleOffset = new Vector3(0, bottleOffset.y + 0.6f, 0);
            }
            else if (bottleCounter % bottlesPerHorizontalLine == 0)
            {
                bottleOffset = new Vector3(bottleOffset.x + 0.6f, bottleOffset.y, 0);
            }

            juice.transform.SetParent(juicesStackPoint);
            var itemJuicesStackPointPosition = bottleOffset;
            juice.transform.DOLocalRotate(Vector3.zero, 3f / itemToShipSpeed);
            juice.transform.DOLocalMove(itemJuicesStackPointPosition, 3f / itemToShipSpeed).SetEase(easeType)
                .OnComplete(() =>
                {

                    // if (Inventory.TryRemoveItem(juice))
                    // {
                    //     _ship.GetComponent<JuicesToMoney>().MoneyMaker(juice);
                    // }

                });
            bottleCounter++;
            bagItemOffsetY -= bagItemOffsetYAmount;
            bottleOffset = new Vector3(bottleOffset.x, bottleOffset.y, bottleOffset.z + 0.6f);
            
            yield return new WaitForSeconds(itemToShipDelay);
        }

        // _ship.GoSellJuicesShipAnimation();
        bottleOffset = new Vector3(0, bottleOffset.y, 0);

        // SortBagItems();
        isShipCoroutineStarted = false;

        yield return new WaitForSeconds(2f);
    }
}
