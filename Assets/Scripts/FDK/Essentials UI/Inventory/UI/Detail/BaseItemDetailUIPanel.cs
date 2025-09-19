using FDK.UI.Base;
using FDK.UI.Base.Interface;

namespace FDK.Inventory
{
    public abstract class BaseItemDetailUIPanel<T> : BaseUIPanel where T : IGameUIData
    {
        public T Item { get; private set; }

        public virtual void Initialize(T item)
        {
            Item = item;
            UpdateUI();
        }

        public override void UpdateUI()
        {
        }
    }
}
