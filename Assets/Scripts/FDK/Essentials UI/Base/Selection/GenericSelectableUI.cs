using FDK.UI.Base.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FDK.UI
{
    public interface IGenericUIData : IGameUIData
    {
        string Id { get; }
    }

    [System.Serializable]
    public class GenericUIData : IGenericUIData
    {
        public string Id { get; private set; }

        public GenericUIData(string id)
        {
            Id = id;
        }
    }

    public class GenericSelectableUI : SelectableUI
    {
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _text;

        public override void UpdateUI()
        {
        }

        public override void Select()
        {
            base.Select();
            IsSelected = true;
            OnSelectedChanged();
        }

        private void OnSelectedChanged()
        {
            _background.gameObject.SetActive(IsSelected);
        }

        public override void Deselect()
        {
            IsSelected = false;
            OnSelectedChanged();
        }

        public override void Confirm()
        {
            base.Confirm();
        }
    }
}
