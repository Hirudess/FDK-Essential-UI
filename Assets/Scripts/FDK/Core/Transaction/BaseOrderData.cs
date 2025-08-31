using FDK.Core.GameData;
using System.Collections.Generic;

namespace FDK.Core.Transaction
{
    public abstract class BaseOrderData
    {
        public Dictionary<string, int> OrderItem;
    }

    public class ShopOrderData : BaseOrderData
    {
    }

    public class CraftingOrderData : BaseOrderData
    {
    }
}
