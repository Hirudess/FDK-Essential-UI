using FDK.GameData;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FDK.Crafting
{
    public class FDKCraftingDetailUI : BaseUiItem
    {
        [SerializeField] private FDKCraftingMaterialUI _materialPrefab;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private RectTransform _root;

        public List<FDKCraftingMaterialUI> MaterialContainers { get; private set; } = new();

        public ICraftingUIData GameData { get; private set; }

        public void SetGameData(ICraftingUIData gameData)
        {
            GameData = gameData;
        }

        public override void UpdateUI()
        {
            if (GameData == null) return;

            _name.text = GameData.Name;
            _description.text = GameData.Description;

            foreach (var item in MaterialContainers)
            {
                item.Hide();
            }

            var idx = 0;
            foreach (var material in GameData.Materials)
            {
                if (idx < MaterialContainers.Count)
                {
                    MaterialContainers[idx].UpdateUI(material);
                    MaterialContainers[idx].Show();
                }
                else
                {
                    var container = Instantiate(_materialPrefab, _root);
                    container.UpdateUI(material);
                    container.Show();

                    MaterialContainers.Add(container);
                }

                idx++;
            }
        }
    }
}
