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

        private void Buy(string id)
        {
            if (_selectedShop == null) return;
            if (!_selectedShop.Products.Contains(id)) return;
            _transactionSystem.Buy(id);
        }

        private void Sell(string id)
        {
            if (_selectedShop == null) return;
            _transactionSystem.Sell(id);
        }

        public void Start()
        {

        }
    }
}
