using FDK.Core;
using System.Collections.Generic;

namespace FDK.Shop
{
    [System.Serializable]
    public class ShopGameData : BaseGameData
    {
        public ShopVisualData ShopVisualData;
        public List<string> Products;
    }

    [System.Serializable]
    public class ShopProductUIData : ISelectableUIData
    {
        public ShopProductUIData(string id, string name, string desc, int price)
        {
            Id = id;
            Name = name;
            Desc = desc;
            Price = price;
        }

        public string Id { get; private set; }
        public string Name;
        public string Desc;
        public int Price;
    }


    [System.Serializable]
    public class ShopVisualData
    {
        public string Name;
        public string Location;
        public string Owner;
    }
}
