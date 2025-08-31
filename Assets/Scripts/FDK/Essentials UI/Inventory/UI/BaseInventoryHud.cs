using FDK.Core.GameData;
using FDK.UI.Base;

namespace FDK.Inventory
{
    public abstract class BaseInventoryHud<T> : BaseUIPanel where T : BaseInventorySlotUiItem
    {
        public override void UpdateUI()
        {
        }
    }
}
