using FDK.Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FDK.Currency
{
    public class CurrenciesTesting : MonoBehaviour
    {

        [SerializeField]
        private Button _addGold;
        [SerializeField]
        private Button _addPremium;

        private ICurrencySystem _currencySystem;
        [Inject]
        public void Inject(ICurrencySystem currencySystem)
        {
            _currencySystem = currencySystem;
        }


        private void Awake()
        {
            _addGold.onClick.AddListener(AddGold);
            _addPremium.onClick.AddListener(AddPremium);
        }


        private void AddGold()
        {
            var randomAdd = UnityEngine.Random.Range(1, 100000);
            _currencySystem.AddCurrency(CurrencyType.Gold, randomAdd);
        }

        private void AddPremium()
        {
            var randomAdd = UnityEngine.Random.Range(1, 100000);
            _currencySystem.AddCurrency(CurrencyType.Premium, randomAdd);
        }
    }
}
