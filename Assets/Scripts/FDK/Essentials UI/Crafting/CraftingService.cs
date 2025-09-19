using FDK.Core;
using FDK.Core.Services;
using FDK.GameData;
using FDK.Shop;
using UnityEngine.Scripting;

namespace FDK.Crafting
{
    public interface ICraftingService
    {
        void Craft(CraftingRecipeGameData craftingRecipeGameData);
        void Initialize(CraftingManager craftingManager);
        void OpenShop(string shopId);
    }

    public class CraftingService : BaseService, ICraftingService
    {
        private readonly ITransactionSystem _transactionSystem;
        private readonly IGameDataCollectionService _gameDataCollectionService;
        private CraftingManager _craftingManager;

        [Preserve]
        public CraftingService(ITransactionSystem transactionSystem, IGameDataCollectionService gameDataCollectionService)
        {
            _transactionSystem = transactionSystem;
            _gameDataCollectionService = gameDataCollectionService;
        }

        public void Initialize(CraftingManager craftingManager)
        {
            _craftingManager = craftingManager;
            SetReady(true);
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

        public void OpenShop(string shopId)
        {
            if (!IsReady) return;

            var shop = _gameDataCollectionService.CraftingShop.GetItem(shopId);
            if (shop == null) return;

            _craftingManager.InitializeCraftingShop(shop);
        }
    }

}


