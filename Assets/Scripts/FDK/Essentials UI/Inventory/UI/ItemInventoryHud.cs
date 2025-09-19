using System.Collections.Generic;
using UnityEngine;

namespace FDK.Inventory
{
    public class ItemInventoryHud : BaseSelectionUIItem<ItemSlotUIData, ItemSlotUIItem>
    {
        [SerializeField]
        private Dictionary<string, ItemSlotUIItem> _gameData = new();

        public void InitializeUI(Dictionary<string, ItemSlotPlayerData> inventoryDict)
        {
            foreach (var kv in inventoryDict)
            {
                var item = kv.Value;
                var isExist = _gameData.ContainsKey(kv.Key);
                var itemUIData = new ItemSlotUIData(null, item.Amount);

                if (isExist)
                {
                    if (_gameData[kv.Key] == null) continue;

                    if (item == null) continue;
                    _gameData[kv.Key].UpdateGameData(itemUIData);
                }
                else
                {
                    var spawnedUI = Instantiate(_selectionUIPrefabs, _content);
                    spawnedUI.Initialize(itemUIData, null);
                    _gameData.Add(kv.Key, spawnedUI);
                }
            }
        }
    }
}
