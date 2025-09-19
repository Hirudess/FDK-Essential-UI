using FDK.Crafting;
using UnityEngine;

namespace FDK.Core.GameData
{
    [CreateAssetMenu(fileName = "Crafting Recipe Shop Game Data", menuName = "FDK/GameData/Crafting Recipes")]
    [System.Serializable]
    public class FDKCraftingRecipeGameDataCollection : BaseGameDataPreset<CraftingRecipeGameDataCollection, CraftingRecipeGameData>
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
