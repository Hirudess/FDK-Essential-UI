using FDK.Core.GameData;
using System.Linq;
using UnityEngine.Scripting;

namespace FDK.Inventory
{
    public interface IPlayerCharacterInventoryService
    {
        int Capacity { get; }

        void Recruit(string id);
        void Release(string id);
    }

    public class PlayerCharacterInventoryService : BaseInventory<CharacterGameData>, IPlayerCharacterInventoryService
    {
        public override int Capacity => 20;

        private readonly CharacterGameDataCollection _characterGameDataCollection;
        [Preserve]
        public PlayerCharacterInventoryService(CharacterGameDataCollection characterGameDataCollection)
        {
            _characterGameDataCollection = characterGameDataCollection;
        }

        public void Recruit(string id)
        {
            var character = GetCharacter(id);
            if (character == null)
            {
                return;
            }

            AddItem(id, character);
        }

        public void Release(string id)
        {
            if (items.ContainsKey(id))
            {
                RemoveItem(id);
            }
        }

        private CharacterGameData GetCharacter(string id)
        {
            return _characterGameDataCollection.Characters.FirstOrDefault(x => x.Id == id);
        }
    }
}
