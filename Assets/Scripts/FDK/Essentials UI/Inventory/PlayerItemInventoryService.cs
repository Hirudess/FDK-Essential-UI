using FDK.Core.GameData;
using UnityEngine.Scripting;

namespace FDK.Inventory
{
    public interface IPlayerItemInventoryService : IBaseInventory<ItemInventorySlotPlayerData, ItemGameData>
    {
    }

    public class PlayerItemInventoryService : BaseInventory<ItemInventorySlotPlayerData, ItemGameData>, IPlayerItemInventoryService
    {
        public override int Capacity => 20;
        [Preserve]
        public PlayerItemInventoryService(ItemGameDataCollection itemGameDataCollection)
        {
        }

        protected override void CreateAndRegisterSlot(ItemGameData item, int amount)
        {
            var inventory = new ItemInventorySlotPlayerData(item, amount);
            Items.Add(item.Id, inventory);
        }
    }
}
