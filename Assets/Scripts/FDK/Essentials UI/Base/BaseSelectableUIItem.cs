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

        public T GameUIData { get; protected set; }

        public bool IsSelected => throw new System.NotImplementedException();


        private void Awake()
        {
            
        }
        public void Initialize(T uiData, System.Action<T> onClick)
        {
            GameUIData = uiData;
            OnButtonClicked = onClick;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => OnClick());
            UpdateUI();
        }

        public void UpdateGameData(T UIData)
        {
            GameUIData = UIData;
            UpdateUI();
        }

        public override void UpdateUI()
        {

        }

        private void OnClick()
        {
            if (GameUIData == null) return;
            OnButtonClicked?.Invoke(GameUIData);
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
