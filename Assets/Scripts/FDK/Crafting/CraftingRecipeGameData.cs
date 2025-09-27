using FDK.Core;
using FDK.Core.GameData;

namespace FDK.Crafting
{
    [System.Serializable]
    public class CraftingRecipeGameData : BaseGameData
    {
        public string Result;
        public CraftingComponent[] RequiredItems;
    }

    [System.Serializable]
    public class CraftingComponent
    {
        public string ItemId;
        public int Amount;
    }
}
