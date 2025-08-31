using FDK.Core;
using FDK.Core.Services;
using FDK.Shop;
using UnityEngine.Scripting;

namespace FDK.Crafting
{
    public interface ICraftingService
    {
        void Craft(CraftingRecipeGameData craftingRecipeGameData);
    }

    public class CraftingService : BaseService, ICraftingService
    {
        private readonly ITransactionSystem _transactionSystem;

        [Preserve]
        public CraftingService(ITransactionSystem transactionSystem)
        {
            _transactionSystem = transactionSystem;
        }

        public void Craft(CraftingRecipeGameData craftingRecipeGameData)
        {
            CommitCraft(craftingRecipeGameData);
        }

        private void CommitCraft(CraftingRecipeGameData craftingRecipeGameData)
        {
            var craftingData = craftingRecipeGameData.ToCraftingTransactionData();
            _transactionSystem.Craft(craftingData);
        }
    }

}


