using FDK.Shop;
using FDK.UI.Base;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FDK.UI
{
    public class ShopUIPanel : BaseUIPanel
    {
        [SerializeField] private ShopSelectionUIHandler _shopUISelectionUIHandler;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _exitButton;

        public UnityEvent OnBuy = new();
        public UnityEvent OnSell = new();

        private void OnBuyButtonPressed()
        {
            OnBuy?.Invoke();
        }

        private void OnSellButtonPressed()
        {
            OnSell?.Invoke();
        }


        public void Initialize(List<ShopProductUIData> shopGameData)
        {
            _shopUISelectionUIHandler.Initialize(shopGameData);
        }
    }
}
