using FDK.Core;

namespace FDK.Inventory
{
    [System.Serializable]
    public abstract class BaseItemGameData : BaseGameData
    {
        public string Name;
        public string Description;

        public virtual int MaxStack => 99;
    }
}

