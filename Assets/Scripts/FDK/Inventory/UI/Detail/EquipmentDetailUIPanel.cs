using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FDK.Inventory
{
    public interface IEquipmentDetailUIData : IItemDetailUIData
    {

    }

    public class EquipmentDetailUIData : IEquipmentDetailUIData
    {
        public EquipmentDetailUIData(Sprite sprite, string name, string description)
        {
            Sprite = sprite;
            Name = name;
            Description = description;
        }

        public Sprite Sprite { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }

    public class EquipmentDetailUIPanel : BaseItemDetailUIPanel<IEquipmentDetailUIData>
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _desc;

        public override void UpdateUI()
        {
            if (Item == null) return;
            _image.sprite = null;
            _name.text = Item.Name;
            _desc.text = Item.Description;
        }
    }
}
