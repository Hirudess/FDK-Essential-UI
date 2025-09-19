using FDK.Core.Assignable;

namespace FDK.Equipment
{
    public abstract class BaseEquipSlot : Assignable
    {
        public void Equip(string id)
        {
            if (!IsValid()) return;
            Assign(id);
        }

        public void Unequip()
        {
            Unassign();
        }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
