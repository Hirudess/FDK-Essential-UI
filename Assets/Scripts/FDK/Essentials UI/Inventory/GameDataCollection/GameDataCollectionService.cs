using FDK.Core.GameData;
using FDK.Core.Services;
using UnityEngine;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace FDK.GameData
{
    [System.Serializable]
    public struct GameDataCollectionRef
    {
        public TextAsset ItemCollection;
        public TextAsset CharacterCollection;
    }

    public interface IGameDataCollectionService
    {
        ItemGameDataCollection ItemCollection { get; }
        CharacterGameDataCollection CharacterCollection { get; }
    }

    public class GameDataCollectionService : BaseService, IGameDataCollectionService, IStartable
    {
        private readonly TextAsset _itemCollectionRef;
        private readonly TextAsset _characterCollectionRef;
        public ItemGameDataCollection ItemCollection { get; private set; }
        public CharacterGameDataCollection CharacterCollection { get; private set; }

        [Preserve]
        public GameDataCollectionService(GameDataCollectionRef reference)
        {
            _itemCollectionRef = reference.ItemCollection;
            _characterCollectionRef = reference.CharacterCollection;
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

            var characterCollection = JsonUtility.FromJson<CharacterGameDataCollection>(_characterCollectionRef.text);
            if (characterCollection == null)
            {
                Debug.LogError($"FDK Core | Failed to get Character data collection {nameof(GameDataCollectionService)}");
                return;
            }
            CharacterCollection = characterCollection;

            SetReady(true);
        }

        public void Start()
        {

        }
    }
}
