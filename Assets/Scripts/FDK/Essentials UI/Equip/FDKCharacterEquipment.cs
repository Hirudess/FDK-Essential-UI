namespace FDK.Equipment
{
    public interface IFDKCharacterEquipment
    {
        ArmorSlot ArmorSlot { get; }
        WeaponSlot WeaponSlot { get; }

        void EquipArmor(string id);
        void EquipWeapon(string id);
        void UnequipArmor();
        void UnequipWeapon();
    }

    public class FDKCharacterEquipment : IFDKCharacterEquipment
    {
        public WeaponSlot WeaponSlot { get; private set; } = new();
        public ArmorSlot ArmorSlot { get; private set; } = new();

        public FDKCharacterEquipment()
        {

        }

        public void EquipWeapon(string id)
        {
            WeaponSlot.Equip(id);
        }

        public void UnequipWeapon()
        {
            WeaponSlot.Unequip();
        }


        public void EquipArmor(string id)
        {
            ArmorSlot.Equip(id);
        }

        public void UnequipArmor()
        {
            ArmorSlot.Unequip();
        }
    }
}
