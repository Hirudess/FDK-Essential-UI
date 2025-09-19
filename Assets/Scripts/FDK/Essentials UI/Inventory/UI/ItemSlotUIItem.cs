using FDK.UI.Base.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FDK.Inventory
{
    #region GameUIData
    public interface IItemSlotUIData : IGameUIData
    {
        Sprite Sprite { get; }
        int Amount { get; }
    }

    public class ItemSlotUIData : IItemSlotUIData
    {
        public ItemSlotUIData(Sprite sprite, int amount)
        {
            Sprite = sprite;
            Amount = amount;
        }

        public Sprite Sprite { get; }
        public int Amount { get; }
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
