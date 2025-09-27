using FDK.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FDK.Crafting
{
    public class FDKCraftingMaterialUI : BaseUiItem
    {
        [SerializeField] private Image _materialImage;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _amountNeeded;

        public void UpdateUI(FDKMaterialUIData materialUIData)
        {
            _name.text = materialUIData.Name;
            _amountNeeded.text = materialUIData.AmountNeeded;
        }
    }
}
