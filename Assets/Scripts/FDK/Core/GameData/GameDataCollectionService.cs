using FDK.Core.GameData;
using FDK.Core.Services;
using FDK.Crafting;
using FDK.Inventory;
using UnityEngine;
using UnityEngine.Scripting;

namespace FDK.GameData
{
    [System.Serializable]
    public struct GameDataCollectionRef
    {
        public TextAsset ItemCollection;
        public TextAsset CharacterCollection;
        public TextAsset ShopCollection;

        public TextAsset CraftingRecipeCollection;
        public TextAsset CraftingShopCollection;
    }

    public interface IGameDataCollectionService
    {
        ItemGameDataCollection ItemCollection { get; }
        CharacterGameDataCollection CharacterCollection { get; }
        ShopGameDataCollection ShopCollection { get; }
        public CraftingRecipeGameDataCollection CraftingRecipe { get; }
        public CraftingGameDataCollection CraftingShop { get; }
    }

    public class GameDataCollectionService : BaseService, IGameDataCollectionService
    {
        private readonly TextAsset _itemCollectionRef;
        private readonly TextAsset _characterCollectionRef;
        private readonly TextAsset _shopCollectionRef;
        private readonly TextAsset _craftRecipeCollectionRef;
        private readonly TextAsset _craftShopCollectionRef;

        public ItemGameDataCollection ItemCollection { get; private set; }
        public CharacterGameDataCollection CharacterCollection { get; private set; }
        public ShopGameDataCollection ShopCollection { get; private set; }
        public CraftingRecipeGameDataCollection CraftingRecipe { get; private set; }
        public CraftingGameDataCollection CraftingShop { get; private set; }

        [Preserve]
        public GameDataCollectionService(GameDataCollectionRef reference)
        {
            _itemCollectionRef = reference.ItemCollection;
            _characterCollectionRef = reference.CharacterCollection;
            _shopCollectionRef = reference.ShopCollection;

            _craftRecipeCollectionRef = reference.CraftingRecipeCollection;
            _craftShopCollectionRef = reference.CraftingShopCollection;
            SerializeCollection();
        }


        private void SerializeCollection()
        {
            var itemCollection = JsonUtility.FromJson<ItemGameDataCollection>(_itemCollectionRef.text);
            if (itemCollection == null)
            {
                Debug.LogError($"FDK Core | Failed to get item data collection {nameof(GameDataCollectionService)}");
                return;
            }
            ItemCollection = itemCollection;
            itemCollection.CreateDict();

            var characterCollection = JsonUtility.FromJson<CharacterGameDataCollection>(_characterCollectionRef.text);
            if (characterCollection == null)
            {
                Debug.LogError($"FDK Core | Failed to get Character data collection {nameof(GameDataCollectionService)}");
                return;
            }
            CharacterCollection = characterCollection;
            CharacterCollection.CreateDict();

            var shopCollection = JsonUtility.FromJson<ShopGameDataCollection>(_shopCollectionRef.text);
            if (characterCollection == null)
            {
                Debug.LogError($"FDK Core | Failed to get Shop data collection {nameof(GameDataCollectionService)}");
                return;
            }
            ShopCollection = shopCollection;
            ShopCollection.CreateDict();

            var recipeCollection = JsonUtility.FromJson<CraftingRecipeGameDataCollection>(_craftRecipeCollectionRef.text);
            if (recipeCollection == null)
            {
                Debug.LogError($"FDK Core | Failed to get Shop data collection {nameof(GameDataCollectionService)}");
                return;
            }

            CraftingRecipe = recipeCollection;
            CraftingRecipe.CreateDict();

            var craftShop = JsonUtility.FromJson<CraftingGameDataCollection>(_craftShopCollectionRef.text);
            if (craftShop == null)
            {
                Debug.LogError($"FDK Core | Failed to get Shop data collection {nameof(GameDataCollectionService)}");
                return;
            }

            CraftingShop = craftShop;
            CraftingShop.CreateDict();

            SetReady(true);
        }
    }
}
