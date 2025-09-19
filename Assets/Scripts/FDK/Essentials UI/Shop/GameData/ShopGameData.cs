using FDK.Core;
using FDK.UI.Base.Interface;
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
    public class ShopUIData : IGameUIData
    {
        public ShopVisualData ShopVisualData;
        public List<string> Products;

        public ShopUIData(ShopGameData shopGameData)
        {
            ShopVisualData = shopGameData.ShopVisualData;
            Products = shopGameData.Products;
        }
    }

    [System.Serializable]
    public class ShopProductUIData : IGameUIData
    {
        public ShopProductUIData(string id, string name, string desc, int price)
        {
            Id = id;
            Name = name;
            Desc = desc;
            Price = price;
        }

        public string Id;
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
