using FDK.Core.Assignable;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace FDK.Equipment
{
    public interface IEquipableData : IAssignableData
    {

    }

    public interface IEquipableSlotUIData : IAssignableSlotData<IEquipableData>
    {

    }

    public class EquipableSlotUIData : IEquipableSlotUIData
    {
        public EquipLocation EquipLocation { get; set; }
        public IEquipableData Item { get; protected set; }
        public bool IsEmpty => Item == null;

        [Preserve]
        public EquipableSlotUIData(EquipLocation equipLocation)
        {
            EquipLocation = equipLocation;
        }

        public void Assign(IEquipableData item)
        {
            Item = item;
        }

        public void Unassign()
        {
            Item = null;
        }
    }


    public abstract class BaseEquipmentSlotUI : BaseAssignableSlotUI<IEquipableSlotUIData, IEquipableData>
    {
        [SerializeField] private Image _background;

        public override void Select()
        {
            _background.gameObject.SetActive(true);
        }

        public override void Deselect()
        {
            _background.gameObject.SetActive(false);
        }
    }
}
