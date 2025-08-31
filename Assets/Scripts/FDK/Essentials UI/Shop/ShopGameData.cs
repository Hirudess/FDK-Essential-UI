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
        public ShopProductUIData(string name , string price)
        {
            Name = name;
            Price = price;
        }
        public string Name;
        public string Price;
    }


    [System.Serializable]
    public class ShopVisualData
    {
        public string Name;
        public string Location;
        public string Owner;
    }
}
