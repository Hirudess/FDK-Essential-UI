using FDK.Core;
using UnityEngine;

namespace FDK.Currency
{
    public class PremiumCurrencyItemView : CurrencyItemView
    {
        public override void InitializeUI(Sprite currencySprite)
        {
            Currency = CurrencyType.Premium;
            base.InitializeUI(currencySprite);
        }
    }
}
