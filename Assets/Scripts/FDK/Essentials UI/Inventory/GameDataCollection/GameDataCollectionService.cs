using FDK.Core.GameData;
using FDK.Core.Services;
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
    }

    public interface IGameDataCollectionService
    {
        ItemGameDataCollection ItemCollection { get; }
        CharacterGameDataCollection CharacterCollection { get; }
        ShopGameDataCollection ShopCollection { get; }
    }

    public class GameDataCollectionService : BaseService, IGameDataCollectionService
    {
        private readonly TextAsset _itemCollectionRef;
        private readonly TextAsset _characterCollectionRef;
        private readonly TextAsset _shopCollectionRef;

        public ItemGameDataCollection ItemCollection { get; private set; }
        public CharacterGameDataCollection CharacterCollection { get; private set; }
        public ShopGameDataCollection ShopCollection { get; private set; }

        [Preserve]
        public GameDataCollectionService(GameDataCollectionRef reference)
        {
            _itemCollectionRef = reference.ItemCollection;
            _characterCollectionRef = reference.CharacterCollection;
            _shopCollectionRef = reference.ShopCollection;
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

            SetReady(true);
        }
    }
}
