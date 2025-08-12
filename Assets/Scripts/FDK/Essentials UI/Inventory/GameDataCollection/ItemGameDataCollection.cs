using Mono.Cecil.Cil;
using System.Collections.Generic;

namespace FDK.Core.GameData
{
    [System.Serializable]
    public class ItemGameDataCollection
    {
        public List<ItemGameData> Items;

        private Dictionary<string, ItemGameData> _itemsDict = new();

        public ItemGameData GetItem(string key)
        {
            if (_itemsDict.TryGetValue(key, out var itemGameData))
            {
                return itemGameData;
            }
            return null;
        }

        public ItemGameDataCollection()
        {
            if (Items == null) Items = new();

            foreach (var item in Items)
            {
                _itemsDict.Add(item.Id, item);
            }

        }
    }
}
