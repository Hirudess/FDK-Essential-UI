using System.Collections.Generic;

namespace FDK.Shop
{
    [System.Serializable]
    public class ShopGameData
    {
        public ShopVisualData ShopVisualData;
        public List<string> Products;
    }

    [System.Serializable]
    public class ShopVisualData
    {
        public string Name;
        public string Location;
        public string Owner;
    }
}
