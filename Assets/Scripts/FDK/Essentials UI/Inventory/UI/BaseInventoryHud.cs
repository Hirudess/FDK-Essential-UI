using FDK.Core.GameData;
using FDK.UI.Base;

namespace FDK.Inventory
{
    public abstract class BaseInventoryHud<T> : BaseUIItemGroup where T : BaseInventorySlotUiItem
    {
        public override void UpdateUI()
        {
        }
    }
}
