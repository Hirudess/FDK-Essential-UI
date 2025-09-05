using UnityEngine;

namespace FDK.Core.GameData
{
    [CreateAssetMenu(fileName = "Item", menuName = "FDK/GameData/Items Collection")]
    [System.Serializable]
    public class FDKItemGameDataCollection : BaseGameDataPreset<ItemGameDataCollection,ItemGameData>
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
