using UnityEngine;

namespace FDK.Inventory
{
    public class ItemInventorySlotUiItem : BaseInventorySlotUiItem
    {
        public void InitializeUI(Sprite sprite, string amount)
        {
            _sprite = sprite;
            _amount.text = amount;
        }

        public void UpdateUI(string amount)
        {
            _amount.text = amount;
        }
    }
}
