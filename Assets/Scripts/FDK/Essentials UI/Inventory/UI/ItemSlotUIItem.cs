using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FDK.Inventory
{
    #region GameUIData
    public interface IItemSlotUIData : ISelectableUIData
    {
        Sprite Sprite { get; }
        int Amount { get; }
    }

    public class ItemSlotUIData : IItemSlotUIData
    {
        public ItemSlotUIData(string id, Sprite sprite, int amount)
        {
            Id = id;
            Sprite = sprite;
            Amount = amount;
        }

        public Sprite Sprite { get; private set; }
        public int Amount { get; private set; }
        public string Id { get; private set; }
    }
    #endregion

    public class ItemSlotUIItem : BaseSelectableUIItem<ItemSlotUIData>
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _amount;

        public override void UpdateUI()
        {
            if (GameUIData == null) return;

            _image.sprite = GameUIData.Sprite;
            _amount.text = GameUIData.Amount.ToString();
        }
    }
}
