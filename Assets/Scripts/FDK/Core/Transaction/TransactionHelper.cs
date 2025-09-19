using FDK.CoreTransaction;
using FDK.Crafting;
using System.Collections.Generic;

namespace FDK.Core
{
    public static class CraftingTransactionDataExtensions
    {
        public static CraftingTransactionData ToCraftingTransactionData(this CraftingRecipeGameData craftingRecipeData)
        {
            var craftingDict = new Dictionary<string, int>();
            foreach (var data in craftingRecipeData.RequiredItems)
            {
                craftingDict.Add(data.ItemId, data.Amount);
            }
            var transactionData = new CraftingTransactionData(craftingDict);
            return transactionData;
        }
    }
}
