using FDK.GameData;
using FDK.UI;
using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace FDK.Shop
{
    [System.Serializable]
    public struct ShopManagerRef
    {
        public ShopUIPanel ShopSelectionUIHandler;
        public ShopConfirmationUIPanel ShopConfirmationUIPanel;
    }

    public class ShopManager : IStartable, IDisposable
    {
        private readonly ITransactionSystem _transactionSystem;
        private readonly IGameDataCollectionService _gameDataCollectionService;
        private readonly IShopService _shopService;
        private readonly ShopUIPanel _shopUIPanel;
        private readonly ShopConfirmationUIPanel _shopConfirmationUIPanel;

        private ShopGameData _shopGameData;
        private bool _disposedValue;

        public bool IsReady => _shopGameData != null;

        [Preserve]
        public ShopManager(ShopManagerRef shopManagerRef,
        IGameDataCollectionService gameDataCollectionService,
        ITransactionSystem transactionSystem,
        IShopService shopService)
        {
            _shopConfirmationUIPanel = shopManagerRef.ShopConfirmationUIPanel;
            _transactionSystem = transactionSystem;
            _gameDataCollectionService = gameDataCollectionService;
            _shopUIPanel = shopManagerRef.ShopSelectionUIHandler;
            _shopService = shopService;

            _shopConfirmationUIPanel.OnProceedEvent.AddListener(Buy);
            _shopService.Initialize(this);
        }

        public void InitializeShop(ShopGameData shopGameData)
        {
            _shopGameData = shopGameData;
            var shops = new List<ShopProductUIData>();
            foreach (var itemId in shopGameData.Products)
            {
                var item = _gameDataCollectionService.ItemCollection.GetItem(itemId);
                if (item == null)
                {
                    Debug.LogError("Cant find " + itemId);
                    continue;
                }
                shops.Add(new ShopProductUIData(itemId, item.Name, item.Description, item.Price));
            }
            _shopUIPanel.Initialize(shopGameData.ShopVisualData, shops);
        }

        public void Buy(string id, int amount)
        {
            if (!IsReady) return;
            if (!_shopGameData.Products.Contains(id)) return;

            var receipt = new Dictionary<string, int>();
            receipt.Add(id, amount);
            _transactionSystem.Buy(receipt);
        }

        public void BulkBuy(Dictionary<string, int> receipt)
        {
            if (!IsReady) return;
            foreach (var item in _shopGameData.Products)
            {
                if (!_shopGameData.Products.Contains(item)) return;
            }
            _transactionSystem.Buy(receipt);
        }

        public void Sell(string id, int amount)
        {
            if (!IsReady) return;
            _transactionSystem.Sell(id, amount);
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
                    _shopConfirmationUIPanel.OnProceedEvent.RemoveListener(Buy);
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
