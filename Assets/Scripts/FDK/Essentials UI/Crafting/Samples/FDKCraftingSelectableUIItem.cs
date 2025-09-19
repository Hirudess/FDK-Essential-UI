using FDK.GameData;
using TMPro;
using UnityEngine;

namespace FDK.Crafting
{
    public class FDKCraftingSelectableUIItem : BaseSelectableUIItem<FDKCraftingUIData>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _desc;

        public override void UpdateUI()
        {
            if (GameUIData == null) return;
            _name.text = GameUIData.Name;
            _desc.text = GameUIData.Description;
        }
    }
}
