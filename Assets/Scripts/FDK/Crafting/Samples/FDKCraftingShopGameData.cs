using FDK.Core;
using FDK.Shop;
using FDK.UI.Base.Interface;
using System.Collections.Generic;

namespace FDK.GameData
{
    [System.Serializable]
    public class FDKCraftingShopGameData : BaseGameData
    {
        public ShopVisualData ShopVisualData;
        public List<string> Products;
    }

    [System.Serializable]
    public class FDKCraftingShopUIData : IGameUIData
    {
        public ShopVisualData ShopVisualData;
        public List<string> Products;

        public FDKCraftingShopUIData(FDKCraftingShopGameData craftingGameData)
        {
            ShopVisualData = craftingGameData.ShopVisualData;
            Products = craftingGameData.Products;
        }
    }
}
