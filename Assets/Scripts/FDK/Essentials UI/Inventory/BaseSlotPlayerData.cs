using FDK.Core;
using FDK.Core.GameData;

namespace FDK.Inventory
{
    public class BaseSlotPlayerData<T> : BasePlayerData where T : BaseItemGameData
    {
        private const int _maxStack = 20;

        public T Item;
        public int Amount;

        public int MaxStack => _maxStack;

        public BaseSlotPlayerData(T gameData, int amount)
        {
            Item = gameData;
            Amount = amount;
        }

        public void AddItem(int amount)
        {
            var finalAmount = Amount + amount;
            Amount = System.Math.Clamp(finalAmount, 0, _maxStack);
        }
    }
}
