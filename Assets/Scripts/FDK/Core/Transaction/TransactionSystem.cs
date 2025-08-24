using FDK.Core;
using FDK.Core.GameData;
using FDK.CoreTransaction;
using FDK.GameData;
using FDK.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

namespace FDK.Shop
{
    public interface ITransactionSystem
    {
        void Buy(Dictionary<string, int> buyReceipt);
        void Craft(CraftingTransactionData craftingTransactionData);
        void GetRewards(Dictionary<string, int> rewardsReceipt);
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

        public void Buy(Dictionary<string, int> buyReceipt)
        {
            var totalCost = CalculateCost(buyReceipt);
            var receipt = new ShopTransactionData(buyReceipt, totalCost);
            CommitShopTransaction(receipt);
        }

        public void GetRewards(Dictionary<string, int> rewardsReceipt)
        {
            var rewards = new RewardsTransactionData(rewardsReceipt);
            CommitRewardTransaction(rewards);
        }

        public void Craft(CraftingTransactionData craftingTransactionData)
        {
            CommitCraftingTransaction(craftingTransactionData);
        }

        private TransactionCostCurrency CalculateCost(Dictionary<string, int> buyReceipt)
        {
            var totalCost = 0;
            foreach (var item in buyReceipt)
            {
                var itemData = GetItem(item.Key);
                if (itemData == null) continue;
                totalCost += itemData.Price * item.Value;
            }

            var costCurrency = new TransactionCostCurrency();
            costCurrency.CurrencyType = CurrencyType.Gold;
            costCurrency.Amount = totalCost;
            return costCurrency;
        }

        private void CommitRewardTransaction(RewardsTransactionData transactionData)
        {
            AddRewards(transactionData);
        }

        private void CommitShopTransaction(ShopTransactionData transactionData)
        {
            var isCostEnough = IsCostEnough(transactionData);
            if (!isCostEnough) return;
            Debug.LogError("CIAT2");
            RemoveCurrency(transactionData.CurrencyCost);
            AddRewards(transactionData);
        }

        private void CommitCraftingTransaction(CraftingTransactionData transactionData)
        {
            var isCostEnough = IsCostEnough(transactionData);
            if (!isCostEnough) return;
            RemoveCurrency(transactionData.CurrencyCost);
            RemoveCost(transactionData);
            AddRewards(transactionData);
        }

        private void AddRewards(TransactionBaseData transactionData)
        {
            foreach (var reward in transactionData.Rewards)
            {
                var item = GetItem(reward.ItemId);
                _playerItemInventoryService.AddItem(item, reward.Amount);
            }
        }

        private void RemoveCost(ShopTransactionData transactionData)
        {
            var shopCost = transactionData.CurrencyCost;
            _currencySystem.RemoveCurrency(shopCost.CurrencyType, shopCost.Amount);
        }

        private void RemoveCost(CraftingTransactionData transactionData)
        {
            foreach (var cost in transactionData.ItemCost)
            {
                _playerItemInventoryService.RemoveItem(cost.ItemId, cost.Amount);
            }

            var shopCost = transactionData.CurrencyCost;
            _currencySystem.RemoveCurrency(shopCost.CurrencyType, shopCost.Amount);
        }

        private void RemoveCurrency(TransactionCostCurrency currencyCost)
        {
            _currencySystem.RemoveCurrency(currencyCost.CurrencyType, currencyCost.Amount);
        }

        private bool IsCostEnough(CraftingTransactionData craftingTransactionData)
        {
            return IsItemCostEnough(craftingTransactionData.ItemCost) && IsCurrencyEnough(craftingTransactionData.CurrencyCost);
        }

        private bool IsCostEnough(RewardsTransactionData transactionData)
        {
            return true;
        }

        private bool IsCostEnough(ShopTransactionData shopTransactionData)
        {
            return IsCurrencyEnough(shopTransactionData.CurrencyCost);
        }

        private bool IsItemCostEnough(TransactionCostItem[] itemCost)
        {
            foreach (var item in itemCost)
            {
                var itemData = GetItem(item.ItemId);
                if (item == null) return false;
            }

            return true;
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

        private bool IsCurrencyEnough(TransactionCostCurrency costCurrency)
        {
            return _currencySystem.HasEnough(costCurrency.CurrencyType, costCurrency.Amount);
        }

        private ItemGameData GetItem(string itemId)
        {
            return _gameDataCollectionService.ItemCollection.Items.FirstOrDefault(x => x.Id == itemId);
        }
    }
}
