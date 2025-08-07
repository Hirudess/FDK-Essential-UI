using FDK.Core;
using FDK.Core.GameData;
using FDK.GameData;
using FDK.Inventory;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

namespace FDK.Shop
{
    public interface ITransactionSystem
    {
        void Buy(string itemId);
        void Sell(string itemId);
    }

    public class TransactionSystem : ITransactionSystem
    {
        private readonly ICurrencySystem _currencySystem;
        private readonly IPlayerItemInventoryService _playerItemInventoryService;
        private readonly IGameDataCollectionService _gameDataCollectionService;


        [Preserve]
        public TransactionSystem(ICurrencySystem currencySystem, IPlayerItemInventoryService playerItemInventoryService, IGameDataCollectionService gameDataCollectionService)
        {
            _currencySystem = currencySystem;
            _playerItemInventoryService = playerItemInventoryService;
            _gameDataCollectionService = gameDataCollectionService;
        }

        public void Buy(string itemId)
        {
            var isInventoryFull = _playerItemInventoryService.IsFull;
            if (isInventoryFull) return;

            var item = GetItem(itemId);
            if (item == null) return;

            if (!IsCurrencyEnough(item.Price)) return;
            CommitBuy(itemId, CurrencyType.Gold, item.Price);
        }

        private void CommitBuy(string itemId, CurrencyType currency, int price)
        {
            _playerItemInventoryService.AddItem(itemId);
            _currencySystem.RemoveCurrency(currency, price);
        }

        public void Sell(string itemId)
        {
            var itemInPossession = _playerItemInventoryService.HasItem(itemId);
            if (!itemInPossession) return;

            var item = GetItem(itemId);
            if (item == null) return;

            var sellPrice = (int)(Mathf.Floor(item.Price / 2f));
            CommitSell(itemId, CurrencyType.Gold, sellPrice);
        }

        private void CommitSell(string itemId, CurrencyType currencyType, int price)
        {
            _playerItemInventoryService.RemoveItem(itemId);
            _currencySystem.AddCurrency(currencyType, price);
        }

        private bool IsCurrencyEnough(int price)
        {
            return _currencySystem.HasEnough(CurrencyType.Gold, price);
        }

        private ItemGameData GetItem(string itemId)
        {
            return _gameDataCollectionService.ItemCollection.Items.FirstOrDefault();
        }
    }
}
