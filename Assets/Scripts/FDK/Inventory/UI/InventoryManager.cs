using FDK.GameData;
using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace FDK.Inventory
{
    [System.Serializable]
    public struct InventoryHudRef
    {
        public ItemSelectionUIItem ItemInventoryHud;
        public ItemDetailUIPanel DetailUIPanel;
    }

    public interface IInventoryManager
    {
        void UpdateUI();
    }

    public class InventoryManager : IStartable, IDisposable, IInventoryManager
    {
        private readonly IPlayerItemInventoryService _playerItemInventoryService;
        private readonly IGameDataCollectionService _gameDataCollectionService;
        private readonly ItemSelectionUIItem _itemSelectionUI;
        private readonly ItemDetailUIPanel _itemDetailUIPanel;
        private bool _disposedValue;

        [Preserve]
        public InventoryManager(IGameDataCollectionService gameDataCollectionService, IPlayerItemInventoryService inventoryService, InventoryHudRef inventoryHudRef)
        {
            _playerItemInventoryService = inventoryService;
            _gameDataCollectionService = gameDataCollectionService;
            _itemSelectionUI = inventoryHudRef.ItemInventoryHud;
            _itemDetailUIPanel = inventoryHudRef.DetailUIPanel;

            _playerItemInventoryService.OnInventoryUpdated.AddListener(UpdateUI);
            _itemSelectionUI.OnSelectionChanged.AddListener(UpdateDetail);
            UpdateUI();
        }

        public void UpdateUI()
        {
            var inventory = _playerItemInventoryService.GetAllItems();
            var uiData = new List<ItemSlotUIData>();
            foreach (var item in inventory)
            {
                var itemUIData = new ItemSlotUIData(item.Value.Item.Id, null, item.Value.Amount);
                uiData.Add(itemUIData);
            }
            _itemSelectionUI.Initialize(uiData);
        }

        public void UpdateDetail(ItemSlotUIData itemSlotUIData)
        {
            var itemSlot = _playerItemInventoryService.GetItem(itemSlotUIData.Id);
            if (itemSlot == null)
            {
                return;
            }
            var itemData = _gameDataCollectionService.ItemCollection.GetItem(itemSlotUIData.Id);
            var detail = new ItemDetailUIData(itemSlotUIData.Id, null, itemData.Name, itemData.Description);
            _itemDetailUIPanel.Initialize(detail);
        }

        public void Start()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _playerItemInventoryService.OnInventoryUpdated.RemoveListener(UpdateUI);
                }
                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
