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
        void Buy(string itemId, int amount);
        void Sell(string itemId, int amount);
    }

    public class TransactionSystem : ITransactionSystem
    {
        private readonly ICurrencySystem _currencySystem;
        private readonly IPlayerItemInventoryService _playerItemInventoryService;
        private readonly IGameDataCollectionService _gameDataCollectionService;


        [Preserve]
        public TransactionSystem(
            ICurrencySystem currencySystem,
            IPlayerItemInventoryService playerItemInventoryService,
            IGameDataCollectionService gameDataCollectionService)
        {
            _currencySystem = currencySystem;
            _playerItemInventoryService = playerItemInventoryService;
            _gameDataCollectionService = gameDataCollectionService;
        }

        public void Buy(string itemId, int amount)
        {
            var isInventoryFull = _playerItemInventoryService.IsFull;
            if (isInventoryFull) return;

            var item = GetItem(itemId);
            if (item == null) return;

            if (!IsCurrencyEnough(item.Price)) return;
            CommitBuy(item, amount, CurrencyType.Gold, item.Price);
        }
        private void CommitBuy(ItemGameData itemGameData, int amount, CurrencyType currency, int price)
        {
            _playerItemInventoryService.AddItem(itemGameData, amount);
            _currencySystem.RemoveCurrency(currency, price);
        }

        public void Sell(string itemId, int amount)
        {
            var itemInPossession = _playerItemInventoryService.HasItem(itemId);
            if (!itemInPossession) return;

            var item = GetItem(itemId);
            if (item == null) return;

            var sellPrice = (int)(Mathf.Floor(item.Price / 2f));
            var totalSellPrice = sellPrice * amount;
            CommitSell(itemId, amount, CurrencyType.Gold, totalSellPrice);
        }

        private void CommitSell(string itemId, int sellAmount, CurrencyType currencyType, int price)
        {
            _playerItemInventoryService.RemoveItem(itemId, sellAmount);
            _currencySystem.AddCurrency(currencyType, price);
        }

        private bool IsCurrencyEnough(int price)
        {
            return _currencySystem.HasEnough(CurrencyType.Gold, price);
        }

        private ItemGameData GetItem(string itemId)
        {
            return _gameDataCollectionService.ItemCollection.Items.FirstOrDefault(x => x.Id == itemId);
        }
    }
}
