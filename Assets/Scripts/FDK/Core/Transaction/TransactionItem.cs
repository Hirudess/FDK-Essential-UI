using FDK.Core;
using System.Collections.Generic;

namespace FDK.CoreTransaction
{
    public abstract class TransactionBaseData
    {
        public TransactionRewardItem[] Rewards { get; set; }

        public TransactionBaseData(Dictionary<string, int> receiptDict)
        {
            Rewards = new TransactionRewardItem[receiptDict.Count];

            var count = 0;
            foreach (var item in receiptDict)
            {
                Rewards[count] = new TransactionRewardItem(item.Key, item.Value);
                count++;
            }
        }
    }

    public class RewardsTransactionData : TransactionBaseData
    {
        public RewardsTransactionData(Dictionary<string, int> receiptDict) : base(receiptDict)
        {
        }
    }

    public class ShopTransactionData : TransactionBaseData
    {
        public ShopTransactionData(Dictionary<string, int> receiptDict, TransactionCostCurrency currencyCost) : base(receiptDict)
        {
            CurrencyCost = currencyCost;
        }

        public TransactionCostCurrency CurrencyCost { get; set; }
    }

    public class CraftingTransactionData : TransactionBaseData
    {
        public TransactionCostItem[] ItemCost;
        public TransactionCostCurrency CurrencyCost;

        public CraftingTransactionData(Dictionary<string, int> receiptDict) : base(receiptDict)
        {
        }
    }

    public class TransactionItem
    {
        public string ItemId;
        public int Amount;
    }

    public class TransactionCostCurrency
    {
        public CurrencyType CurrencyType;
        public int Amount;
    }

    public class TransactionRewardItem : TransactionItem
    {
        public TransactionRewardItem(string id, int amount)
        {
            ItemId = id;
            Amount = amount;
        }
    }

    public class TransactionCostItem : TransactionItem
    {

    }
}
