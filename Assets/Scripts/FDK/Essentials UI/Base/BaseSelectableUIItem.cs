using FDK.UI.Base.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace FDK
{
    public abstract class BaseSelectableUIItem<T> : BaseUiItem, IBaseSelectableUiItem where T : IGameUIData
    {
        [SerializeField] protected Image _background;

        [SerializeField] protected Button _button;

        public System.Action<T> OnButtonClicked;

        public T GameData { get; private set; }

        public bool IsSelected => throw new System.NotImplementedException();

        public void Initialize(T campaignData, System.Action<T> onClick)
        {
            GameData = campaignData;
            OnButtonClicked = onClick;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => OnClick());
            UpdateUI();
        }

        public override void UpdateUI()
        {

        }

        private void OnClick()
        {
            if (GameData == null) return;
            OnButtonClicked?.Invoke(GameData);
            Select();
        }

        public virtual void Select()
        {
            if (IsBlocked) return;
            _background.gameObject.SetActive(true);
        }

        public virtual void Deselect()
        {
            if (IsBlocked) return;
            _background.gameObject.SetActive(false);
        }
    }
}
