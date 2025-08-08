using FDK.Core.GameData;
using TacticsRPG.Game.GameData;

namespace FDK.Crafting
{
    [System.Serializable]
    public class CraftingRecipeGameData : BaseGameData
    {
        public CraftingComponent[] RequiredItems;
        public ItemGameData Result;
    }

    [System.Serializable]
    public class CraftingComponent
    {
        public ItemGameData Item;
        public int Amount;
    }
}
