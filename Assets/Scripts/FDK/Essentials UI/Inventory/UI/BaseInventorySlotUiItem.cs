using TMPro;
using UnityEngine;

namespace FDK.Inventory
{
    public abstract class BaseInventorySlotUiItem : BaseUiItem
    {
        [SerializeField]
        protected RectTransform _rectTransform;
        [SerializeField]
        protected Sprite _sprite;
        [SerializeField]
        protected TMP_Text _amount;
    }
}
