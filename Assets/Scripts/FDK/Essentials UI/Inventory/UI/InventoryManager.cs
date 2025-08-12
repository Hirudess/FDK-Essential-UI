using System;
using System.Diagnostics;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace FDK.Inventory
{
    [System.Serializable]
    public struct InventoryHudRef
    {
        public ItemInventoryHud ItemInventoryHud;
    }

    public interface IInventoryManager
    {
        void UpdateUI();
    }

    public class InventoryManager : IStartable, IDisposable, IInventoryManager
    {
        private readonly IPlayerItemInventoryService _playerItemInventoryService;
        private readonly ItemInventoryHud _itemInventoryHud;
        private bool _disposedValue;

        [Preserve]
        public InventoryManager(IPlayerItemInventoryService inventoryService, InventoryHudRef inventoryHudRef)
        {
            _playerItemInventoryService = inventoryService;
            _itemInventoryHud = inventoryHudRef.ItemInventoryHud;

            _playerItemInventoryService.OnInventoryUpdated.AddListener(UpdateUI);
        }

        public void UpdateUI()
        {
            var inventory = _playerItemInventoryService.GetAllItems();
            _itemInventoryHud.InitializeUI(inventory);
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
