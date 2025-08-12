using System.Collections.Generic;
using UnityEngine;

namespace FDK.Inventory
{
    public class ItemInventoryHud : MonoBehaviour
    {
        [SerializeField]
        private ItemSlotUIItem _slotPrefab;
        [SerializeField]
        private RectTransform _root;
        private Dictionary<string, ItemSlotUIItem> _gameData = new();

        public void InitializeUI(Dictionary<string, ItemSlotPlayerData> inventoryDict)
        {
            foreach (var kv in inventoryDict)
            {
                var item = kv.Value;
                var isExist = _gameData.ContainsKey(kv.Key);
                if (isExist)
                {
                    if (_gameData[kv.Key] == null) continue;

                    if (item == null) continue;
                    _gameData[kv.Key].UpdateUI(item.Amount.ToString());
                }
                else
                {
                    var spawnedUI = Instantiate(_slotPrefab, _root);
                    spawnedUI.InitializeUI(null, item.Amount.ToString());

                    _gameData.Add(kv.Key, spawnedUI);
                }
            }
        }
    }
}
