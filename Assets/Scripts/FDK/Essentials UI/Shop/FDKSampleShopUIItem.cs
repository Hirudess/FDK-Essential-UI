using TMPro;
using UnityEngine;

namespace FDK.Shop
{
    public class FDKSampleShopUIItem : BaseSelectableUIItem<ShopProductUIData>
    {
        [SerializeField]
        private TMP_Text _itemName;
        [SerializeField]
        private TMP_Text _description;
        [SerializeField]
        private TMP_Text _price;

        public override void UpdateUI()
        {
            if (GameUIData == null) return;
            _itemName.text = GameUIData.Name;
            _description.text = GameUIData.Desc;
            _price.text = GameUIData.Price.ToString();
        }
    }
}
