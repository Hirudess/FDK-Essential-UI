using FDK.GameData;
using FDK.Shop;
using FDK.UI.Base;
using System.Collections.Generic;
using UnityEngine;

namespace FDK.Inventory
{
    public class ItemInventoryUIPanel : BaseUIPanel
    {
        [SerializeField]
        private ItemSelectionUIItem _inventorySelection;
        [SerializeField]
        private ItemDetailUIPanel _itemDetailUIPanel;

        private void Awake()
        {
            _inventorySelection.OnSelectionChanged.AddListener(UpdateDetail);
        }

        private void UpdateDetail(ItemSlotUIData itemDetailUIData)
        {
          //  _itemDetailUIPanel.Initialize(itemDetailUIData);
        }
    }
}
