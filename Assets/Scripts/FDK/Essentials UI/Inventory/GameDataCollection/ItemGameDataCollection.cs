using System.Collections.Generic;

namespace FDK.Core.GameData
{
    [System.Serializable]
    public class ItemGameDataCollection : BaseGameDataCollection<ItemGameData>
    {
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
            if (Collections == null) Collections = new();

            foreach (var item in Collections)
            {
                _itemsDict.Add(item.Id, item);
            }
        }
    }
}
