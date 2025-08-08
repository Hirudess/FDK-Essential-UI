using FDK.GameData;
using FDK.Inventory;
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
        private Button _removeItem;

        [SerializeField]
        private TMP_Dropdown _dropDownGameData;
        [SerializeField]
        private TMP_Dropdown _dropDownInventory;

        private IPlayerItemInventoryService _playerItemInventoryService;
        private IGameDataCollectionService _gameDataCollectionService;

        [Inject]
        public void Inject(IGameDataCollectionService gameDataCollectionService, IPlayerItemInventoryService playerItemInventoryService)
        {
            _playerItemInventoryService = playerItemInventoryService;
            _gameDataCollectionService = gameDataCollectionService;

            var getAllInventory = GetAllItemOption();
            _dropDownGameData.AddOptions(getAllInventory);

            var getAllOwnedItem = GetAllOwnedItem();
            _dropDownInventory.AddOptions(getAllOwnedItem);

            _addItem.onClick.AddListener(OnAddItempressed);
        }

        private void OnAddItempressed()
        {
            var idx = _dropDownGameData.value;
            var item = GetAllItemOption()[idx];

            Debug.Log(item);
        }

        private List<string> GetAllItemOption()
        {
            var options = new List<string>();
            foreach (var item in _gameDataCollectionService.ItemCollection.Items)
            {
                options.Add($"{item.Id}-{item.Name}");
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
