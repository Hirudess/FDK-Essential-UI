using FDK.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FDK.Currency
{
    public abstract class CurrencyItemView : BaseUiItem
    {
        [SerializeField]
        protected Image _currencyImage;
        [SerializeField]
        protected TMP_Text _currencyAmount;

        protected CurrencyType Currency;
        private bool _isReady;

        public virtual void InitializeUI(Sprite currencySprite)
        {
            _currencyImage.sprite = currencySprite;
            _isReady = true;
        }

        public virtual void UpdateUI(CurrencyType currencyType, int amount)
        {
            if (!_isReady) return;
            if (Currency != currencyType) return;
            _currencyAmount.text = amount.ToString();
        }
    }
}
