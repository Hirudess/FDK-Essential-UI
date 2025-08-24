using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace FDK.Shop
{
    public class ShopManager : IStartable
    {
        private readonly ITransactionSystem _transactionSystem;
        private ShopGameData _selectedShop;

        public bool IsReady => _selectedShop != null;

        [Preserve]
        public ShopManager(ITransactionSystem transactionSystem)
        {
            _transactionSystem = transactionSystem;
        }


        public void InitializeShop(ShopGameData shopGameData)
        {
            _selectedShop = shopGameData;
        }

        private void Buy(string id, int amount)
        {
            if (_selectedShop == null) return;
            if (!_selectedShop.Products.Contains(id)) return;

            var receipt = new Dictionary<string, int>();
            receipt.Add(id, amount);
            _transactionSystem.Buy(receipt);
        }

        private void BulkBuy(Dictionary<string, int> receipt)
        {
            if (_selectedShop == null) return;
            foreach (var item in _selectedShop.Products)
            {
                if (!_selectedShop.Products.Contains(item)) return;
            }
            _transactionSystem.Buy(receipt);
        }

        private void Sell(string id, int amount)
        {
            if (_selectedShop == null) return;
            _transactionSystem.Sell(id, amount);
        }

        public void Start()
        {

        }
    }
}
