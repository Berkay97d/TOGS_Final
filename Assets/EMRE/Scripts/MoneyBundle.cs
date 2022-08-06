using IdleCashSystem.Core;

namespace EMRE.Scripts
{
    public class MoneyBundle : Item
    {
        public IdleCash Value { get; private set; }


        public void SetValue(IdleCash value)
        {
            Value = value;
        }

        public void Deposit()
        {
            Balance.Add(Value);
            Destroy();
        }
    }
}