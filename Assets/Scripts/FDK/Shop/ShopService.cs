using FDK.Core.Services;
using FDK.GameData;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace FDK.Shop
{
    public interface IShopService
    {
        void Initialize(ShopManager shopManager);
        void OpenShop(string shopId);
    }

    public class ShopService : BaseService, IStartable, IShopService
    {
        private readonly IGameDataCollectionService _gameDataCollectionService;
        private ShopManager _shopManager;

        [Preserve]
        public ShopService(IGameDataCollectionService gameDataCollectionService)
        {
            _gameDataCollectionService = gameDataCollectionService;
        }

        public void Initialize(ShopManager shopManager)
        {
            _shopManager = shopManager;
            SetReady(true);
        }

        public void OpenShop(string shopId)
        {
            if (!IsReady) return;

            var shop = _gameDataCollectionService.ShopCollection.GetItem(shopId);
            if (shop == null) return;

            _shopManager.InitializeShop(shop);
        }

        public void Start()
        {

        }
    }
}
