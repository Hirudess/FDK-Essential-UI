using FDK.Core.GameData;
using System.Linq;
using UnityEngine.Scripting;

namespace FDK.Inventory
{
    public interface IPlayerItemInventoryService
    {
        int Capacity { get; }

        void AddItem(string id);
        void Release(string id);
    }

    public class PlayerItemInventoryService : BaseInventory<ItemGameData>, IPlayerItemInventoryService
    {
        public override int Capacity => 20;

        private readonly ItemGameDataCollection _itemGameDataCollection;
        [Preserve]
        public PlayerItemInventoryService(ItemGameDataCollection itemGameDataCollection)
        {
            _itemGameDataCollection = itemGameDataCollection;
        }

        public void AddItem(string id)
        {
            var item = GetItemData(id);
            if (item == null)
            {
                return;
            }

            base.AddItem(id, item);
        }

        public void Release(string id)
        {
            if (items.ContainsKey(id))
            {
                RemoveItem(id);
            }
        }

        private ItemGameData GetItemData(string id)
        {
            return _itemGameDataCollection.Items.FirstOrDefault(x => x.Id == id);
        }
    }
}
