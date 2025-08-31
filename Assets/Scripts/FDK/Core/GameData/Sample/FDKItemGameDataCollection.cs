using UnityEngine;

namespace FDK.Core.GameData
{
    [CreateAssetMenu(fileName = "Item", menuName = "FDK/GameData/Items Collection")]
    [System.Serializable]
    public class FDKItemGameDataCollection : BaseGameDataPreset<ItemGameDataCollection,ItemGameData>
    {
    }
}
