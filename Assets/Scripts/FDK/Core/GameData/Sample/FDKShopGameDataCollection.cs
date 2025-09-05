using FDK.Shop;
using UnityEngine;

namespace FDK.Core.GameData
{
    [CreateAssetMenu(fileName = "Shop", menuName = "FDK/GameData/Shops Collection")]
    [System.Serializable]
    public class FDKShopGameDataCollection : BaseGameDataPreset<ShopGameDataCollection, ShopGameData>
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
