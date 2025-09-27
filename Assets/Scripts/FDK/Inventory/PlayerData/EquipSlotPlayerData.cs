using FDK.Core.GameData;

namespace FDK.Inventory
{
    public class EquipSlotPlayerData : BaseSlotPlayerData<EquipGameData>
    {
        public EquipSlotPlayerData(EquipGameData gameData, int amount) : base(gameData, amount)
        {
        }
    }
}
