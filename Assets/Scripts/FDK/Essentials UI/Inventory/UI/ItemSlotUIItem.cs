using UnityEngine;

namespace FDK.Inventory
{
    public class ItemSlotUIItem : BaseInventorySlotUiItem
    {
        public void InitializeUI(Sprite sprite, string amount)
        {
            _image.sprite = sprite;
            _amount.text = amount;
        }

        public void UpdateUI(string amount)
        {
            _amount.text = amount;
        }
    }
}
