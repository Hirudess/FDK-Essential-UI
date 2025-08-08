using FDK.Core.GameData;
using UnityEngine.Scripting;

namespace FDK.Inventory
{
    public interface IPlayerItemInventoryService : IBaseInventory<ItemSlotPlayerData, ItemGameData>
    {

    }

    public class PlayerItemInventoryService : BaseInventory<ItemSlotPlayerData, ItemGameData>, IPlayerItemInventoryService
    {
        public override int Capacity => 20;
        [Preserve]
        public PlayerItemInventoryService()
        {
        }

        protected override void CreateAndRegisterSlot(ItemGameData item, int amount)
        {
            var inventory = new ItemSlotPlayerData(item, amount);
            Items.Add(item.Id, inventory);
        }
    }
}
