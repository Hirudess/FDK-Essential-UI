using FDK.Shop;
using System.Collections.Generic;

namespace FDK.Core.GameData
{
    [System.Serializable]
    public class ShopGameDataCollection : BaseGameDataCollection<ShopGameData>
    {
        private Dictionary<string, ShopGameData> _itemsDict = new();

        public ShopGameData GetItem(string key)
        {
            if (_itemsDict.TryGetValue(key, out var itemGameData))
            {
                return itemGameData;
            }
            return null;
        }

        public ShopGameDataCollection()
        {
            if (Collections == null) Collections = new();

            foreach (var shop in Collections)
            {
                _itemsDict.Add(shop.Id, shop);
            }
        }
    }
}
