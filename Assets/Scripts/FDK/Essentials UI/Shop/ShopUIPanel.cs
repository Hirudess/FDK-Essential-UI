using FDK.Shop;
using FDK.UI.Base;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FDK.UI
{
    public class ShopUIPanel : BaseUIPanel
    {
        [SerializeField] private ShopSelectionUIHandler _shopUISelectionUIHandler;
        [SerializeField] private ShopConfirmationUIPanel _shopConfirmationUIPanel;
        [SerializeField] private TMP_Text _shopName;
        [SerializeField] private TMP_Text _sellerName;
        [SerializeField] private TMP_Text _introDialogue;

        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _sellButton;

        public UnityEvent OnBuy = new();
        public UnityEvent OnSell = new();

        private void Awake()
        {
            _buyButton.onClick.AddListener(OnBuyButtonPressed);
        }

        private void OnBuyButtonPressed()
        {
            OnBuy?.Invoke();

            var product = _shopUISelectionUIHandler.SelectedGameData;
            _shopConfirmationUIPanel.Initialize(product);
        }

        private void OnSellButtonPressed()
        {
            OnSell?.Invoke();
        }


        public void Initialize(ShopVisualData shopVisualData, List<ShopProductUIData> shopGameData)
        {
            _shopUISelectionUIHandler.Initialize(shopGameData);
            _shopName.text = shopVisualData.Name;
            _sellerName.text = shopVisualData.Owner;

            _introDialogue.text = PlaceholderShopIntroDialogue.GetPlaceholderShopIntro();
        }
    }
}
