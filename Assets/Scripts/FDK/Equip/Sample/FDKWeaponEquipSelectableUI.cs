namespace FDK.Equipment
{
    public interface IWeaponSlotUIData : IEquipableData
    {

    }

    public class WeaponSelectableUIData : IWeaponSlotUIData
    {
        public WeaponSelectableUIData(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            EquipLocation = EquipLocation.Weapon;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public EquipLocation EquipLocation { get; private set; }

        public bool IsEmpty => string.IsNullOrEmpty(Id);
    }

    public class FDKWeaponEquipSelectableUI : BaseEquipmentSlotUI
    {
        private void Awake()
        {
            AssignableSlot = new EquipableSlotUIData(EquipLocation.Weapon);
        }
    }

    public enum EquipLocation
    {
        Weapon = 0,
        Body = 1,
    }
}
