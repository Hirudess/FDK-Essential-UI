namespace FDK.Equipment
{
    public interface IArmorSlotUIData : IEquipableData
    {

    }

    public class ArmorSelectableUIData : IArmorSlotUIData
    {
        public ArmorSelectableUIData(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            EquipLocation = EquipLocation.Body;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public EquipLocation EquipLocation { get; private set; }

        public bool IsEmpty => string.IsNullOrEmpty(Id);
    }

    public class FDKArmorEquipSelectableUI : BaseEquipmentSlotUI
    {
        private void Awake()
        {
            AssignableSlot = new EquipableSlotUIData(EquipLocation.Body);
        }
    }
}
