using FDK.Core;
using UnityEngine;

namespace FDK.Currency
{
    public class NormalCurrencyItemView : CurrencyItemView
    {
        public override void InitializeUI(Sprite currencySprite)
        {
            Currency = CurrencyType.Gold;
            base.InitializeUI(currencySprite);
        }
    }
}
