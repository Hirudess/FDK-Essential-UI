using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FDK.Inventory
{
    public abstract class BaseInventorySlotUiItem : BaseUiItem
    {
        [SerializeField]
        protected RectTransform _rectTransform;
        [SerializeField]
        protected Image _image;
        [SerializeField]
        protected TMP_Text _amount;
    }
}
