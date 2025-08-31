using TMPro;
using UnityEngine;

namespace FDK.Shop
{
    public class FDKSampleShopUIItem : BaseSelectableUIItem<ShopProductUIData>
    {
        [SerializeField]
        private TMP_Text _itemName;
        [SerializeField]
        private TMP_Text _price;

        public override void UpdateUI()
        {
            if (GameData == null) return;
            _itemName.text = GameData.Name;
            _price.text = GameData.Price;
        }
    }
}
