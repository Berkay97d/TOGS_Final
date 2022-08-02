using TMPro;
using UnityEngine;

namespace UpgradeSystem.Scripts.Test
{
    public class DummyCoinUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text amountField;


        private void Start()
        {
            UpdateAmountField(DummyCoin.Amount);
        }

        private void OnEnable()
        {
            DummyCoin.OnChanged += UpdateAmountField;
        }

        private void OnDisable()
        {
            DummyCoin.OnChanged -= UpdateAmountField;
        }


        public void AddCoin(int amount)
        {
            DummyCoin.Add(amount);
        }

        public void RemoveCoin(int amount)
        {
            DummyCoin.TryRemove(amount);
        }


        private void UpdateAmountField(int amount)
        {
            amountField.text = amount.ToString();
        }
    }
}
