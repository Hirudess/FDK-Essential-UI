using FDK.UI.Base.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FDK.Inventory
{
    public interface IItemDetailUIData : IGameUIData
    {
        Sprite Sprite { get; }
        string Name { get; }
        string Description { get; }
    }

    public class ItemDetailUIData : IItemDetailUIData
    {
        public ItemDetailUIData(string id, Sprite sprite, string name, string description)
        {
            Id = id;
            Sprite = sprite;
            Name = name;
            Description = description;
        }

        public Sprite Sprite { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Id { get; private set; }
    }


    public class ItemDetailUIPanel : BaseItemDetailUIPanel<IItemDetailUIData>
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
