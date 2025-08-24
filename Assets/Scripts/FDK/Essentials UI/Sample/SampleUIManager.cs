using FDK.Core;
using FDK.Core.GameData;
using FDK.Core.SaveFile;
using FDK.GameData;
using FDK.Inventory;
using FDK.Shop;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FDK.Sample
{
    public class SampleUIManager : MonoBehaviour
    {
        [SerializeField]
        private Button _addItem;
        [SerializeField]
        private Button _buyItem;
        [SerializeField]
        private TMP_InputField _amountField;
        [SerializeField]
        private Button _removeItem;
        [SerializeField]
        private Button _save;

        [SerializeField]
        private TMP_InputField _amountCurrencyField;
        [SerializeField]
        private Button _addCurrency;

        [SerializeField]
        private TMP_Dropdown _dropDownGameData;
        [SerializeField]
        private TMP_Dropdown _dropDownInventory;

        private IPlayerItemInventoryService _playerItemInventoryService;
        private IGameDataCollectionService _gameDataCollectionService;
        private ISaveLoadFileSystemService _saveLoadFileSystemService;
        private ITransactionSystem _transactionSystem;
        private ICurrencySystem _currencySystem;

        private Dictionary<int, ItemGameData> _gameDataDictionary = new();

        [Inject]
        public void Inject(IGameDataCollectionService gameDataCollectionService,
        IPlayerItemInventoryService playerItemInventoryService, ISaveLoadFileSystemService saveLoadFileSystemService, ITransactionSystem transactionSystem, ICurrencySystem currencySystem)
        {
            _playerItemInventoryService = playerItemInventoryService;
            _gameDataCollectionService = gameDataCollectionService;
            _saveLoadFileSystemService = saveLoadFileSystemService;
            _transactionSystem = transactionSystem;
            _currencySystem = currencySystem;

            var getAllInventory = GetAllItemOption();
            _dropDownGameData.AddOptions(getAllInventory);
            _amountField.contentType = TMP_InputField.ContentType.IntegerNumber;

            var getAllOwnedItem = GetAllOwnedItem();
            _dropDownInventory.AddOptions(getAllOwnedItem);

            _addItem.onClick.AddListener(OnAddItemPressed);
            _buyItem.onClick.AddListener(OnBuyItemPressed);
            _addCurrency.onClick.AddListener(OnAddCurrency);
            _save.onClick.AddListener(Save);
        }

        private void OnAddItemPressed()
        {
            var idx = _dropDownGameData.value;
            var amount = int.Parse(_amountField.text);
            if (amount <= 0) return;
            if (_gameDataDictionary.TryGetValue(idx, out var data))
            {
                var dict = new Dictionary<string, int>();
                dict.Add(data.Id, amount);
                _transactionSystem.GetRewards(dict);
            }
        }

        private void OnAddCurrency()
        {
            var amount = int.Parse(_amountCurrencyField.text);
            if (amount <= 0) return;

            _currencySystem.AddCurrency(CurrencyType.Gold, amount);
            var total = _currencySystem.GetCurrencyAmount(CurrencyType.Gold);
            Debug.Log($"Added {amount} Gold. Gold  = {total} ");
        }

        private void OnBuyItemPressed()
        {
            var idx = _dropDownGameData.value;
            var amount = int.Parse(_amountField.text);
            if (amount <= 0) return;

            if (_gameDataDictionary.TryGetValue(idx, out var data))
            {
                Debug.Log("CIAT");
                var dict = new Dictionary<string, int>();
                dict.Add(data.Id, amount);
                _transactionSystem.Buy(dict);
            }
        }

        private void Save()
        {
            _saveLoadFileSystemService.Save();
        }

        private List<string> GetAllItemOption()
        {
            var idx = 0;
            var options = new List<string>();
            foreach (var item in _gameDataCollectionService.ItemCollection.Items)
            {
                _gameDataDictionary.Add(idx, item);
                options.Add($"{item.Id}-{item.Name}");
                idx++;
            }

            return options;
        }

        private List<string> GetAllOwnedItem()
        {
            var options = new List<string>();
            foreach (var item in _playerItemInventoryService.GetAllItems())
            {
                options.Add($"{item.Key}-{item.Value.Item.Name}");
            }

            return options;
        }
    }
}
