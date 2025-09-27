using FDK.Core.GameData;

namespace FDK.Inventory
{
    public class ItemSlotPlayerData : BaseSlotPlayerData<ItemGameData>
    {
        public ItemSlotPlayerData(ItemGameData gameData, int amount) : base(gameData, amount)
        {
        }
    }
}
