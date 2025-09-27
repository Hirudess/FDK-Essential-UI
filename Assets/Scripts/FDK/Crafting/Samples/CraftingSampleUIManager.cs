using FDK.GameData;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FDK.Crafting.Sample
{
    public class CraftingSampleUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _shopDropDown;
        [SerializeField] private Button _openShop;

        private IGameDataCollectionService _gameDataCollectionService;
        private ICraftingService _craftingService;

        private bool _isInjected;

        [Inject]
        public void Inject(IGameDataCollectionService gameDataCollectionService, ICraftingService shopService)
        {
            if (_isInjected) return;
            _isInjected = true;

            _gameDataCollectionService = gameDataCollectionService;
            _craftingService = shopService;

            var getAllShopOption = GetAllItemOption();
            _shopDropDown.AddOptions(getAllShopOption);
            _openShop.onClick.AddListener(OpenShop);
        }

        public void OpenShop()
        {
            var idx = _shopDropDown.value;
            var selectedText = _shopDropDown.options[idx].text;
            _craftingService.OpenShop(selectedText);
        }

        private List<string> GetAllItemOption()
        {
            var idx = 0;
            var options = new List<string>();

            foreach (var shop in _gameDataCollectionService.CraftingShop.Collections)
            {
                options.Add($"{shop.Id}");
                idx++;
            }

            return options;
        }
    }
}

