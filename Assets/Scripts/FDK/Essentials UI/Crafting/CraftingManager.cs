using FDK.GameData;
using FDK.Shop;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace FDK.Crafting
{
    [System.Serializable]
    public struct CraftingShopManagerRef
    {
        public FDKCraftingUIPanel CraftingShopUIPanel;
    }

    public class CraftingManager : IStartable, IDisposable
    {
        private readonly ITransactionSystem _transactionSystem;
        private readonly IGameDataCollectionService _gameDataCollectionService;
        private readonly ICraftingService _craftingService;
        private readonly FDKCraftingUIPanel _craftingUIPanel;

        private FDKCraftingShopGameData _craftingShopGameData;
        private bool _disposedValue;

        public bool IsReady => _craftingShopGameData != null;

        [Preserve]
        public CraftingManager(CraftingShopManagerRef craftingManagerRef,
        IGameDataCollectionService gameDataCollectionService,
        ITransactionSystem transactionSystem,
        ICraftingService craftingService)
        {
            _craftingUIPanel = craftingManagerRef.CraftingShopUIPanel;

            _transactionSystem = transactionSystem;
            _gameDataCollectionService = gameDataCollectionService;
            _craftingService = craftingService;
            _craftingService.Initialize(this);
        }

        public void InitializeCraftingShop(FDKCraftingShopGameData craftingShopGameData)
        {
            _craftingShopGameData = craftingShopGameData;
            var craftingShops = new List<FDKCraftingUIData>();
            foreach (var recipeId in craftingShopGameData.Products)
            {
                var recipe = _gameDataCollectionService.CraftingRecipe.GetItem(recipeId);
                if (recipe == null)
                {
                    Debug.LogError("Cant find " + recipeId);
                    continue;
                }

                var materials = new List<FDKMaterialUIData>();
                foreach (var material in recipe.RequiredItems)
                {
                    var mat = _gameDataCollectionService.ItemCollection.GetItem(material.ItemId);
                    if (mat == null) continue;
                    materials.Add(new FDKMaterialUIData(mat.Name, material.Amount.ToString()));
                }
                var result = _gameDataCollectionService.ItemCollection.GetItem(recipe.Result);
                if (result == null) return;
                craftingShops.Add(new FDKCraftingUIData(result.Name, result.Description, materials));
            }

            _craftingUIPanel.Initialize(craftingShopGameData.ShopVisualData,craftingShops);
        }

        public void Buy(string id, int amount)
        {
            if (!IsReady) return;
            if (!_craftingShopGameData.Products.Contains(id)) return;

            var receipt = new Dictionary<string, int>();
            receipt.Add(id, amount);
            _transactionSystem.Buy(receipt);
        }

        public void Start()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
