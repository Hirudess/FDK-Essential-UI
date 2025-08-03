using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace FDK.Core
{
    public interface ICurrencySystem
    {
        event Action<CurrencyType, int> OnCurrencyChanged;

        void AddCurrency(CurrencyType type, int amount);
        int GetCurrencyAmount(CurrencyType type);
        Dictionary<CurrencyType, int> GetSaveData();
        bool HasEnough(CurrencyType type, int amount);
        void LoadSaveData(Dictionary<CurrencyType, int> saveData);
        bool RemoveCurrency(CurrencyType type, int amount);
        void RefreshCurrencies();
    }

    [System.Serializable]
    public class CurrencySystem : ICurrencySystem
    {
        private Dictionary<CurrencyType, int> _currencyWallet = new Dictionary<CurrencyType, int>();
        public event Action<CurrencyType, int> OnCurrencyChanged;

        public CurrencySystem()
        {
            foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            {
                _currencyWallet[type] = 0;
            }
        }

        public void AddCurrency(CurrencyType type, int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning($"Tried to add negative currency! Use {nameof(RemoveCurrency)} instead.");
                return;
            }

            _currencyWallet[type] += amount;
            OnCurrencyChanged?.Invoke(type, _currencyWallet[type]);
        }

        public bool RemoveCurrency(CurrencyType type, int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot remove negative currency!");
                return false;
            }

            if (_currencyWallet[type] < amount)
            {
                Debug.LogWarning($"Not enough {type}!");
                return false;
            }

            _currencyWallet[type] -= amount;
            OnCurrencyChanged?.Invoke(type, _currencyWallet[type]);
            return true;
        }

        public bool HasEnough(CurrencyType type, int amount)
        {
            return _currencyWallet[type] >= amount;
        }

        public int GetCurrencyAmount(CurrencyType type)
        {
            return _currencyWallet[type];
        }

        public void RefreshCurrencies()
        {
            foreach (var currency in _currencyWallet)
            {
                OnCurrencyChanged?.Invoke(currency.Key, currency.Value);
            }
        }

        public Dictionary<CurrencyType, int> GetSaveData() => new Dictionary<CurrencyType, int>(_currencyWallet);
        public void LoadSaveData(Dictionary<CurrencyType, int> saveData) => _currencyWallet = new Dictionary<CurrencyType, int>(saveData);
    }

    public enum CurrencyType
    {
        Gold,
        Gems,
        Souls,
        Premium,
    }
}