using FDK.Crafting;
using FDK.GameData;
using UnityEngine;

namespace FDK.Core.GameData
{
    [CreateAssetMenu(fileName = "Crafting Shop Game Data", menuName = "FDK/GameData/Crafting Shop")]
    [System.Serializable]
    public class FDKCraftingShopGameDataCollection : BaseGameDataPreset<CraftingGameDataCollection, FDKCraftingShopGameData>
    {
        [ContextMenu("Save As Json")]
        public void SaveAsJsonWrapper()
        {
            SaveAsJson();
        }

        [ContextMenu("Load JSON")]
        public void LoadJsonWrapper()
        {
            LoadJson();
        }
    }
}
