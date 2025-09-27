using FDK.Core;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace FDK.Currency
{
    [System.Serializable]
    public struct CurrencyUIReference
    {
        public NormalCurrencyItemView NormalCurrencyItemView;
        public PremiumCurrencyItemView PremiumCurrencyItemView;
    }

    public interface ICurrencyUIManager
    {

    }

    public class PrimaryCurrencyUIManager : IStartable, ICurrencyUIManager
    {
        private readonly CurrencyItemView _normalCurrencyItemView;
        private readonly CurrencyItemView _premiumCurrencyItemView;
        private readonly ICurrencySystem _currencySystem;

        [Preserve]
        public PrimaryCurrencyUIManager(ICurrencySystem currencySystem, CurrencyUIReference currencies)
        {
            _currencySystem = currencySystem;
            _currencySystem.OnCurrencyChanged += UpdateCurrency;

            _normalCurrencyItemView = currencies.NormalCurrencyItemView;
            _premiumCurrencyItemView = currencies.PremiumCurrencyItemView;

            _normalCurrencyItemView.InitializeUI(null);
            _premiumCurrencyItemView.InitializeUI(null);

            _currencySystem.RefreshCurrencies();
        }

        public void Start()
        {

        }

        private void UpdateCurrency(CurrencyType currencyType, int amount)
        {
            switch (currencyType)
            {
                case CurrencyType.Gold:
                    _normalCurrencyItemView.UpdateUI(currencyType, amount);
                    break;
                case CurrencyType.Premium:
                    _premiumCurrencyItemView.UpdateUI(currencyType, amount);
                    break;
            }
        }
    }
}
