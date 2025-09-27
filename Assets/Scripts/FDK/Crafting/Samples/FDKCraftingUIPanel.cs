using FDK.GameData;
using FDK.Shop;
using FDK.UI.Base;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FDK.Crafting
{
    public class FDKCraftingUIPanel : BaseUIPanel
    {
        [SerializeField] private FDKCraftingSelectionUI _craftingSelectionUI;
        [SerializeField] private FDKCraftingDetailUI _craftingDetailUI;

        [SerializeField] private TMP_Text _introDialogue;

        private void Awake()
        {
            _craftingSelectionUI.OnSelectionChanged.AddListener(UpdateDetail);
        }

        private void UpdateDetail(FDKCraftingUIData fDKCraftingUIData)
        {
            _craftingDetailUI.SetGameData(fDKCraftingUIData);
            _craftingDetailUI.UpdateUI();
        }

        public void Initialize(ShopVisualData shopVisualData, List<FDKCraftingUIData> shopGameData)
        {
            _craftingSelectionUI.Initialize(shopGameData);
            _introDialogue.text = PlaceholderShopIntroDialogue.GetPlaceholderShopIntro();
        }
    }
}
