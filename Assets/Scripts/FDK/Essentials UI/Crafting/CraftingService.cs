using FDK.Core.Services;
using FDK.Inventory;
using UnityEngine.Scripting;

namespace FDK.Crafting
{
    public class CraftingService : BaseService
    {
        private readonly PlayerItemInventoryService _playerItemInventoryService;

        [Preserve]
        public CraftingService(PlayerItemInventoryService playerItemInventoryService)
        {
            _playerItemInventoryService = playerItemInventoryService;
        }

        public void Craft(CraftingRecipeGameData craftingRecipeGameData)
        {
            if (!ReadyToCraft(craftingRecipeGameData)) return;

            CommitCraft(craftingRecipeGameData);
        }

        private void CommitCraft(CraftingRecipeGameData craftingRecipeGameData)
        {
            foreach (var component in craftingRecipeGameData.RequiredItems)
            {
                _playerItemInventoryService.RemoveItem(component.Item.Id, component.Amount);
            }

            _playerItemInventoryService.AddItem(craftingRecipeGameData.Result, 1);
        }

        private bool ReadyToCraft(CraftingRecipeGameData craftingRecipeGameData)
        {
            foreach (var component in craftingRecipeGameData.RequiredItems)
            {
                var hasItem = _playerItemInventoryService.HasItem(component.Item.Id);
                if (!hasItem) return false;
            }
            return true;
        }
    }

}


