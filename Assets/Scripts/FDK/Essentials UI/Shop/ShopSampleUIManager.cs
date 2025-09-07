using FDK.GameData;
using FDK.Shop;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FDK.Sample
{
    public class ShopSampleUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _shopDropDown;
        [SerializeField] private Button _openShop;

        private IGameDataCollectionService _gameDataCollectionService;
        private IShopService _shopService;

        private bool _isInjected;

        [Inject]
        public void Inject(IGameDataCollectionService gameDataCollectionService, IShopService shopService)
        {
            if (_isInjected) return;
            _isInjected = true;

            _gameDataCollectionService = gameDataCollectionService;
            _shopService = shopService;

            var getAllShopOption = GetAllItemOption();
            _shopDropDown.AddOptions(getAllShopOption);
            _openShop.onClick.AddListener(OpenShop);
        }

        public void OpenShop()
        {
            var idx = _shopDropDown.value;
            var selectedText = _shopDropDown.options[idx].text;
            _shopService.OpenShop(selectedText);
        }

        private List<string> GetAllItemOption()
        {
            var idx = 0;
            var options = new List<string>();

            foreach (var shop in _gameDataCollectionService.ShopCollection.Collections)
            {
                options.Add($"{shop.Id}");
                idx++;
            }

            return options;
        }
    }
}
