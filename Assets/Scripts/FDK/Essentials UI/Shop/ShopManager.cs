using FDK.GameData;
using FDK.UI;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace FDK.Shop
{
    public struct ShopManagerRef
    {
        public ShopUIPanel ShopSelectionUIHandler;
    }

    public class ShopManager : IStartable
    {
        private readonly ITransactionSystem _transactionSystem;
        private readonly IGameDataCollectionService _gameDataCollectionService;
        private readonly ShopUIPanel _shopUIPanel;

        private ShopGameData _shopGameData;

        public bool IsReady => _shopGameData != null;

        [Preserve]
        public ShopManager(ShopManagerRef shopManagerRef, IGameDataCollectionService gameDataCollectionService, ITransactionSystem transactionSystem)
        {
            _transactionSystem = transactionSystem;
            _gameDataCollectionService = gameDataCollectionService;
            _shopUIPanel = shopManagerRef.ShopSelectionUIHandler;
        }

        public void InitializeShop(ShopGameData shopGameData)
        {
            _shopGameData = shopGameData;
            var shops = new List<ShopProductUIData>();
            foreach (var itemId in shopGameData.Products)
            {
                var item = _gameDataCollectionService.ItemCollection.GetItem(itemId);
                shops.Add(new ShopProductUIData(item.Name, item.Price.ToString()));
            }

            _shopUIPanel.Initialize(shops);
        }

        public void Buy(string id, int amount)
        {
            if (_shopGameData == null) return;
            if (!_shopGameData.Products.Contains(id)) return;

            var receipt = new Dictionary<string, int>();
            receipt.Add(id, amount);
            _transactionSystem.Buy(receipt);
        }

        public void BulkBuy(Dictionary<string, int> receipt)
        {
            if (_shopGameData == null) return;
            foreach (var item in _shopGameData.Products)
            {
                if (!_shopGameData.Products.Contains(item)) return;
            }
            _transactionSystem.Buy(receipt);
        }

        public void Sell(string id, int amount)
        {
            if (_shopGameData == null) return;
            _transactionSystem.Sell(id, amount);
        }

        public void Start()
        {

        }
    }
}
