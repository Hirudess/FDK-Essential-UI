using FDK.Inventory;
using UnityEngine;

namespace FDK.Core.GameData
{
    [CreateAssetMenu(fileName = "Character Game Data", menuName = "FDK/GameData/Character Collection")]
    [System.Serializable]
    public class FDKCharacterGameDataCollection : BaseGameDataPreset<CharacterGameDataCollection, CharacterGameData>
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
