using FDK.Dialogue;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FDK.Currency
{
    public class CurrenciesLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private CurrencyUIReference _currencyUIReference;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_currencyUIReference);
            builder.RegisterEntryPoint<PrimaryCurrencyUIManager>(Lifetime.Singleton).As<ICurrencyUIManager>();
        }
    }
}
