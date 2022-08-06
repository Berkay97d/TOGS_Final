using System;
using System.Collections;
using System.Collections.Generic;
using IdleCashSystem.Core;
using UnityEngine;

namespace EMRE.Scripts
{
    public class MoneyBundle : Item
    {
        private Transform player;
        public Vector3 x;

        protected override void Start()
        {
            base.Start();
            player = GameObject.Find("Player").transform;
        }

        public IdleCash Value { get; private set; }


        public void SetValue(IdleCash value)
        {
            Value = value;
        }

        public void Deposit()
        {
            Balance.Add(Value);
            MoveMoneyBundleToCashUI();
            //Destroy();
        }

        public void MoveMoneyBundleToCashUI()
        {
            
            Vector3 screenPoint = BalanceCounter.Position + new Vector3(0,0,10);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

            StartCoroutine(MoneyBundleCoroutine(worldPos));
        }
        private IEnumerator MoneyBundleCoroutine(Vector3 worldPos)
        {
            float progress = 0;
            while (true)
            {
                progress += Time.deltaTime;

                if (progress < 0.5f)
                {
                    transform.position = 
                        Vector3.Lerp(
                            transform.position, 
                            new Vector3(worldPos.x, worldPos.y, player.position.z), 
                            progress);
                }
                else
                {
                    Destroy(gameObject);
                    break;
                
                }
            
                yield return null;
            }
        
        }
    }
}