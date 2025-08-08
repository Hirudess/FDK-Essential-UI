using FDK.Core.GameData;

namespace FDK.Inventory
{
    public class ItemInventorySlotPlayerData : InventorySlotPlayerData<ItemGameData>
    {
        public ItemInventorySlotPlayerData(ItemGameData gameData, int amount) : base(gameData, amount)
        {
        }
    }
}
